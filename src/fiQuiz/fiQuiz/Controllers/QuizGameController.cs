using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using System.Web;
using fiQuiz.Core;
using fiQuiz.Models;
using fiQuiz.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Options;
using MySql.Data.MySqlClient;

namespace fiQuiz.Controllers
{
    [Route("api/[controller]/[action]")]
    [Authorize()]
    [ApiController]
    public class QuizGameController : ControllerBase
    {
        public static readonly List<QuizQuestionAnswerOption> AnswerOptions = Enum.GetValues(typeof(QuizQuestionAnswerOption))
            .OfType<QuizQuestionAnswerOption>().ToList();
        public static readonly List<QuizJoker> Jokers = Enum.GetValues(typeof(QuizJoker))
            .OfType<QuizJoker>().ToList();

        private readonly ApplicationDbContext _context;
        private readonly ApplicationOptions _options;

        public QuizGameController(ApplicationDbContext context, IOptions<ApplicationOptions> options)
        {
            _context = context;
            _options = options.Value;
        }

        private async Task SelectQuizQuestionAnswers(QuizQuestion quizQuestion)
        {
            if (quizQuestion.QuizQuestionAnswers == null)
                quizQuestion.QuizQuestionAnswers = new List<QuizQuestionAnswer>();
            List<QuestionAnswer> questionAnswers = await _context.QuestionAnswers.Where(x => x.QuestionId == quizQuestion.QuestionId).OrderByDescending(x => x.IsCorrect).ThenBy(x => Guid.NewGuid())
                .Take(AnswerOptions.Count).ToListAsync();
            questionAnswers = questionAnswers.OrderBy(x => Guid.NewGuid()).ToList();
            for (var answerIndex = 0; answerIndex < AnswerOptions.Count; answerIndex++)
            {
                QuizQuestionAnswerOption answerOption = AnswerOptions[answerIndex];
                QuestionAnswer questionAnswer = questionAnswers[answerIndex];
                QuizQuestionAnswer quizQuestionAnswer = new QuizQuestionAnswer
                {
                    QuizQuestion = quizQuestion,
                    Answer = questionAnswer,
                    Option = answerOption
                };
                if (questionAnswer.IsCorrect)
                    quizQuestion.CorrectAnswer = answerOption;

                quizQuestion.QuizQuestionAnswers.Add(quizQuestionAnswer);
            }
        }

        public class QuestionSelection
        {
            public int QuestionId { get; set; }
            public int QuestionIndex { get; set; }
        }

        public async Task<IActionResult> Start(QuizGameStartRequest request)
        {
            int questionCount = _options.QuizQuestionCount;
            QuizGameStartResponse response = new QuizGameStartResponse
            {
                QuestionCount = questionCount
            };
            Quiz quiz = new Quiz
            {
                CompletedFlag = false,
                QuizQuestions = new List<QuizQuestion>()
            };

            List<Question> questions = await _context.SelectQuestions(questionCount).ToListAsync(); ;
            for (var qi = 0; qi < questions.Count; qi++)
            {
                Question question = questions[qi];
                QuizQuestion quizQuestion = new QuizQuestion
                {
                    Quiz = quiz,
                    QuizId = quiz.Id,
                    QuestionNumber = qi + 1,
                    Question = question,
                    QuestionId = question.Id
                };

                await SelectQuizQuestionAnswers(quizQuestion);

                quiz.QuizQuestions.Add(quizQuestion);
            }

            _context.Quizzes.Add(quiz);
            await _context.SaveChangesAsync();
            response.QuizId = quiz.Id;
            response.Jokers = Jokers.Select(x => new QuizGameJokerInfo { Joker = x, JokerName = x.GetDisplayName() }).ToList();

            return Ok(response);
        }

        private async Task<(Quiz, QuizQuestion, int, IActionResult)> GetQuestionBase(int quizId, int quizQuestionId)
        {
            Quiz quiz = await _context.Quizzes.Include(x => x.QuizUsedJokers).Include(x => x.QuizQuestions).ThenInclude(x => x.Question).ThenInclude(x => x.Answers).Include(x => x.QuizQuestions).ThenInclude(x => x.Question).ThenInclude(x => x.Category).Include(x => x.QuizQuestions).ThenInclude(x => x.QuizQuestionAnswers).SingleOrDefaultAsync(x => x.Id == quizId);
            if (quiz == null)
                return (null, null, -1, NotFound("Yarışma bilgisi bulunamadı."));
            QuizQuestion quizQuestion = quizQuestionId == 0 ? quiz.QuizQuestions.OrderBy(x => x.QuestionNumber).FirstOrDefault() :
                quiz.QuizQuestions.SingleOrDefault(x => x.Id == quizQuestionId);

            if (quizQuestion == null)
                return (null, null, -1, NotFound("Soru bulunamadı."));

            int quizQuestionIndex = quiz.QuizQuestions.OrderBy(x => x.QuestionNumber).IndexOf(quizQuestion);

            if (quizQuestionIndex > 0)
            {
                QuizQuestion prevQuizQuestion = quiz.QuizQuestions.OrderBy(x => x.QuestionNumber).ElementAtOrDefault(quizQuestionIndex - 1);
                if (prevQuizQuestion == null)
                    return (null, null, -1, NotFound("Önceki soru bilgisine ulaşılamadı."));
                if (prevQuizQuestion.Answer != prevQuizQuestion.CorrectAnswer)
                    return (null, null, -1, BadRequest("Önceki soruya doğru yanıt verilmediği için yeni soruya geçilemez."));
            }

            return (quiz, quizQuestion, quizQuestionIndex, null);
        }

        private QuizGameGetQuestionResponse CreateQuestionResponse(QuizQuestion quizQuestion)
        {
            QuizGameGetQuestionResponse response = new QuizGameGetQuestionResponse();
            response.QuizId = quizQuestion.QuizId;
            response.QuizQuestionId = quizQuestion.Id;
            response.CategoryName = quizQuestion.Question.Category.Name;
            response.QuestionText = HttpUtility.HtmlDecode(quizQuestion.Question.QuestionText);
            response.Answers = quizQuestion.QuizQuestionAnswers
                .Select(x => new QuizGameAnswer { Answer = HttpUtility.HtmlDecode(x.Answer.Answer), AnswerOption = x.Option }).ToList();
            response.AnswerTime = _options.QuizAnswerTime;
            return response;
        }
        public async Task<IActionResult> GetQuestion(QuizGameGetQuestionRequest request)
        {
            QuizQuestion quizQuestion;
            IActionResult actionResult;
            (_, quizQuestion, _, actionResult) = await GetQuestionBase(request.QuizId, request.QuizQuestionId);

            if (actionResult != null)
                return actionResult;

            if (quizQuestion.ShowedAt != null)
                return BadRequest("Soru tekrar görüntülenemez.");

            QuizGameGetQuestionResponse response = CreateQuestionResponse(quizQuestion);
            quizQuestion.ShowedAt = DateTime.Now;
            await _context.SaveChangesAsync();
            return Ok(response);
        }

        public async Task<IActionResult> SendAnswer(QuizGameSendAnswerRequest request)
        {
            QuizGameSendAnswerResponse response = new QuizGameSendAnswerResponse();
            Quiz quiz;
            QuizQuestion quizQuestion;
            int quizQuestionIndex;
            IActionResult actionResult;
            (quiz, quizQuestion, quizQuestionIndex, actionResult) = await GetQuestionBase(request.QuizId, request.QuizQuestionId);

            if (actionResult != null)
                return actionResult;

            if (quizQuestion.ShowedAt == null)
                return BadRequest("Görüntülenmemiş soruya cevap verilemez.");

            if (quizQuestion.Answer != null && quizQuestion.AnsweredAt != null)
                return BadRequest("Soruya tekrar cevap verilemez.");

            bool hasDoubleAnswerJoker = _context.QuizUsedJokers.Any(x =>
                x.QuizId == quiz.Id && x.QuizQuestionId == quizQuestion.Id && x.Joker == QuizJoker.DoubleAnswer);

            bool isTimeExpired = (DateTime.Now - quizQuestion.ShowedAt.Value).TotalSeconds >
                                 (_options.QuizAnswerTime + _options.QuizAnswerTimeNetworkTolerance);


            bool isDoubleAnswerJokerFirstAnswer = hasDoubleAnswerJoker && quizQuestion.AnsweredAt == null;

            if (!isTimeExpired)
                quizQuestion.Answer = request.AnswerOption;
            quizQuestion.AnsweredAt = DateTime.Now;
            response.IsCorrect = quizQuestion.CorrectAnswer == request.AnswerOption;

            if (isDoubleAnswerJokerFirstAnswer && !response.IsCorrect)
            {
                quizQuestion.Answer = null;
            }
            else if (!response.IsCorrect || quizQuestionIndex == quiz.QuizQuestions.Count - 1)
            {
                response.IsCompleted = true;
                quiz.CompletedAt = DateTime.Now;
                quiz.CompletedFlag = true;
                quiz.CompletionType =
                    response.IsCorrect ? QuizCompletionType.Successful : QuizCompletionType.WrongAnswer;
            }
            else
            {
                var nextQuizQuestion =
                    quiz.QuizQuestions.OrderBy(x => x.QuestionNumber).ElementAt(quizQuestionIndex + 1);
                response.NextQuizQuestionId = nextQuizQuestion.Id;
                response.NextQuizQuestionNumber = nextQuizQuestion.QuestionNumber;
            }
            await _context.SaveChangesAsync();

            return Ok(response);
        }

        public async Task<IActionResult> UseJoker(QuizGameUseJokerRequest request)
        {
            QuizGameUseJokerResponse response = new QuizGameUseJokerResponse();
            Quiz quiz;
            QuizQuestion quizQuestion;
            int quizQuestionIndex;
            IActionResult actionResult;
            (quiz, quizQuestion, quizQuestionIndex, actionResult) = await GetQuestionBase(request.QuizId, request.QuizQuestionId);

            if (actionResult != null)
                return actionResult;

            if (quiz.QuizUsedJokers.Any(x => x.Joker == request.Joker))
                return BadRequest(
                    string.Format("{0} joker hakkı daha önce kullanıldı.", request.Joker.GetDisplayName()));

            quiz.QuizUsedJokers.Add(new QuizUsedJoker
            {
                QuizId = request.QuizId,
                QuizQuestionId = request.QuizQuestionId,
                Joker = request.Joker
            });

            await _context.SaveChangesAsync();

            switch (request.Joker)
            {
                case QuizJoker.ChangeQuestion:
                    Question newQuestion = await _context.SelectQuestionAsync(_options.QuizQuestionCount, quizQuestion.QuestionNumber, quizQuestion.QuestionId);
                    if (newQuestion == null) return BadRequest("Soru değiştirilirken hata oluştu.");
                    _context.QuizQuestionAnswers.RemoveRange(quizQuestion.QuizQuestionAnswers);
                    quizQuestion.Question = newQuestion;
                    quizQuestion.QuestionId = newQuestion.Id;
                    quizQuestion.ShowedAt = DateTime.Now;
                    await SelectQuizQuestionAnswers(quizQuestion);
                    await _context.SaveChangesAsync();
                    response.Question = CreateQuestionResponse(quizQuestion);
                    break;
                case QuizJoker.HalfAndHalf:
                    response.Question = CreateQuestionResponse(quizQuestion);
                    foreach (QuizGameAnswer wrongAnswer in response.Question.Answers.Where(x => x.AnswerOption != quizQuestion.CorrectAnswer).OrderBy(x => Guid.NewGuid()).Take(2))
                    {
                        wrongAnswer.IsWrongAnswer = true;
                    }
                    if (quizQuestion.ShowedAt != null)
                        quizQuestion.ShowedAt = quizQuestion.ShowedAt.Value.AddSeconds(_options.QuizAnswerTimeNetworkTolerance);
                    break;
                case QuizJoker.DoubleAnswer:
                    if (quizQuestion.ShowedAt != null)
                        quizQuestion.ShowedAt = quizQuestion.ShowedAt.Value.AddSeconds(_options.QuizAnswerTimeNetworkTolerance);
                    break;
                case QuizJoker.AdditionalTime:
                    quizQuestion.ShowedAt = DateTime.Now;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            await _context.SaveChangesAsync();

            response.QuizQuestionId = quizQuestion.Id;
            response.QuizId = quiz.Id;

            return Ok(response);
        }

        public async Task<IActionResult> SetTimeout(QuizGameSetTimeoutRequest request)
        {
            QuizGameSetTimeoutResponse response = new QuizGameSetTimeoutResponse();
            Quiz quiz = await _context.Quizzes.FindAsync(request.QuizId);

            if (quiz == null)
                return NotFound("Yarışma bilgisi bulunamadı.");

            if (quiz.CompletedFlag)
                return BadRequest("Yarışma daha önce bitirilmiş.");

            quiz.CompletedFlag = true;
            quiz.CompletedAt = DateTime.Now;
            quiz.CompletionType = QuizCompletionType.Timeout;

            await _context.SaveChangesAsync();

            return Ok(response);
        }

        public async Task<IActionResult> GetUserQuizList(QuizGameGetUserQuizListRequest request)
        {
            QuizGameGetUserQuizListResponse response = new QuizGameGetUserQuizListResponse();
            IQueryable<UserQuiz> quizzesQueryable = _context.Quizzes.Include(x => x.QuizQuestions)
                .Where(x => x.CompletedFlag && x.CompletionType != null && x.CreatedBy == User.Identity.Name).Select(
                    x => new UserQuiz
                    {
                        StartedAt = x.CreatedAt,
                        CompletedAt = x.CompletedAt.Value,
                        CompletionType = x.CompletionType.Value,
                        CorrectAnswerCount = x.QuizQuestions.Count(y => y.Answer == y.CorrectAnswer)
                    });
            switch (request.ListType)
            {
                case UserQuizListType.Last:
                    quizzesQueryable = quizzesQueryable.OrderByDescending(x => x.StartedAt);
                    break;
                case UserQuizListType.Top:
                    quizzesQueryable = quizzesQueryable.OrderByDescending(x => x.CorrectAnswerCount).ThenByDescending(x => x.StartedAt);
                    break;
            }
            response.Quizzes = await quizzesQueryable.Take(5).ToListAsync();
            foreach (UserQuiz userQuiz in response.Quizzes)
            {
                userQuiz.CompletionTypeText = userQuiz.CompletionType.GetDisplayName();
            }
            return Ok(response);
        }

        public async Task<IActionResult> ReportQuestion(QuizGameReportQuestionRequest request)
        {
            QuizQuestion quizQuestion = await _context.QuizQuestions.Include(x=>x.Question).FirstOrDefaultAsync(x => x.QuizId == request.QuizId && x.Id == request.QuizQuestionId);
            Question question = quizQuestion?.Question;
            if (question == null)
                return BadRequest("Soru bulunamadı.");
            QuizGameReportQuestionResponse response = new QuizGameReportQuestionResponse();
            await _context.QuestionReports.AddAsync(new QuestionReport
            {
                Question = question,
                QuestionId = question.Id,
                Description = request.Description,
                IsClosed = false
            });
            return Ok(response);
        }
    }
}
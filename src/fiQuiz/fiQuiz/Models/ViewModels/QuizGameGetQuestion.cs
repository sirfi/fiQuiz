using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace fiQuiz.Models.ViewModels
{
    public class QuizGameGetQuestionRequest: ApiRequestBase
    {
        public int QuizId { get; set; }
        public int QuizQuestionId { get; set; }
    }

    public class QuizGameGetQuestionResponse : ApiResponseBase
    {
        public int QuizId { get; set; }
        public int QuizQuestionId { get; set; }
        public string QuestionText { get; set; }
        public string CategoryName { get; set; }
        public List<QuizGameAnswer> Answers { get; set; }
        public int AnswerTime { get; set; }
    }

    public class QuizGameAnswer
    {
        public QuizQuestionAnswerOption AnswerOption { get; set; }
        public string Answer { get; set; }
        public bool IsWrongAnswer { get; set; }
    }
}

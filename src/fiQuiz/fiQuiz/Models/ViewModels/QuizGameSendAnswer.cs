using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace fiQuiz.Models.ViewModels
{
    public class QuizGameSendAnswerRequest : ApiRequestBase
    {
        public int QuizId { get; set; }
        public int QuizQuestionId { get; set; }
        public QuizQuestionAnswerOption? AnswerOption { get; set; }

    }
    public class QuizGameSendAnswerResponse : ApiResponseBase
    {
        public int QuizId { get; set; }
        public int? NextQuizQuestionId { get; set; }
        public int? NextQuizQuestionNumber { get; set; }
        public bool IsCorrect { get; set; }
        public bool IsCompleted { get; set; }

    }
}

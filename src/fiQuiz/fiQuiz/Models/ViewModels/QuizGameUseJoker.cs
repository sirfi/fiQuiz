using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace fiQuiz.Models.ViewModels
{
    public class QuizGameUseJokerRequest : ApiRequestBase
    {
        public int QuizId { get; set; }
        public int QuizQuestionId { get; set; }
        public QuizJoker Joker { get; set; }
    }
    public class QuizGameUseJokerResponse : ApiResponseBase
    {
        public int QuizId { get; set; }
        public int QuizQuestionId { get; set; }
        public QuizGameGetQuestionResponse Question { get; set; }
    }
}

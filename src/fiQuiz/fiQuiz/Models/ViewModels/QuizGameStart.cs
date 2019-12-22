using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace fiQuiz.Models.ViewModels
{
    public class QuizGameStartRequest : ApiRequestBase
    {

    }
    public class QuizGameStartResponse : ApiResponseBase
    {
        public int QuizId { get; set; }
        public int QuestionCount { get; set; }
        public List<QuizGameJokerInfo> Jokers { get; set; }
    }

    public class QuizGameJokerInfo
    {
        public QuizJoker Joker { get; set; }
        public string JokerName { get; set; }
        public bool IsUsed { get; set; }
    }
}

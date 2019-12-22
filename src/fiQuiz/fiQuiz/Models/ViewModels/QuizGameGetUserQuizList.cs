using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace fiQuiz.Models.ViewModels
{
    public class QuizGameGetUserQuizListRequest : ApiRequestBase
    {
        public UserQuizListType ListType { get; set; }
    }

    public enum UserQuizListType
    {
        Last,
        Top
    }
    public class QuizGameGetUserQuizListResponse : ApiResponseBase
    {
        public List<UserQuiz> Quizzes { get; set; }
    }

    public class UserQuiz
    {
        public DateTime StartedAt { get; set; }
        public DateTime CompletedAt { get; set; }
        public QuizCompletionType CompletionType { get; set; }
        public string CompletionTypeText { get; set; }
        public int CorrectAnswerCount { get; set; }
    }
}

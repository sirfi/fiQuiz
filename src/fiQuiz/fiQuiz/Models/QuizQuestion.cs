using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace fiQuiz.Models
{
    public class QuizQuestion : BaseEntity
    {
        public int QuizId { get; set; }
        public int QuestionNumber { get; set; }
        public Quiz Quiz { get; set; }
        public int QuestionId { get; set; }
        public Question Question { get; set; }
        public QuizQuestionAnswerOption CorrectAnswer { get; set; }
        public QuizQuestionAnswerOption? Answer { get; set; }
        public DateTime? AnsweredAt { get; set; }
        public IList<QuizQuestionAnswer> QuizQuestionAnswers { get; set; }
        public IList<QuizUsedJoker> QuizUsedJokers { get; set; }
        public DateTime? ShowedAt { get; set; }
    }
}

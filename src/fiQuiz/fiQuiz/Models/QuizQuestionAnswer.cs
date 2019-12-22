using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace fiQuiz.Models
{
    public class QuizQuestionAnswer : BaseEntity
    {
        public int QuizQuestionId { get; set; }
        public QuizQuestion QuizQuestion { get; set; }
        public QuizQuestionAnswerOption Option { get; set; }
        public int AnswerId { get; set; }
        public QuestionAnswer Answer { get; set; }
    }
}

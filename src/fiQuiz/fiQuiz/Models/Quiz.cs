using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace fiQuiz.Models
{
    public class Quiz : BaseEntity
    {
        public bool CompletedFlag { get; set; }
        public DateTime? CompletedAt { get; set; }
        public QuizCompletionType? CompletionType { get; set; }

        public IList<QuizQuestion> QuizQuestions { get; set; }
        public IList<QuizUsedJoker> QuizUsedJokers { get; set; }

    }

    public enum QuizCompletionType
    {
        [Display(Name = "Başarılı")]
        Successful,
        [Display(Name = "Yanlış cevap")]
        WrongAnswer,
        [Display(Name = "Süre bitti")]
        Timeout
    }
}

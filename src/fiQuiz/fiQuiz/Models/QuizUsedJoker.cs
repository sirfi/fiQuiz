using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace fiQuiz.Models
{
    public class QuizUsedJoker : BaseEntity
    {
        public int QuizId { get; set; }
        public Quiz Quiz { get; set; }
        public int QuizQuestionId { get; set; }
        public QuizQuestion QuizQuestion { get; set; }
        public QuizJoker Joker { get; set; }
    }

    public enum QuizJoker
    {
        [Display(Name = "Soru Değiştir")]
        ChangeQuestion,
        [Display(Name = "Yarı Yarıya")]
        HalfAndHalf,
        [Display(Name = "Çift Cevap")]
        DoubleAnswer,
        [Display(Name = "Ek Süre")]
        AdditionalTime
    }
}

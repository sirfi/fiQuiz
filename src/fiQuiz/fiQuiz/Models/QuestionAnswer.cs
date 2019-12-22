using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace fiQuiz.Models
{
    public class QuestionAnswer : BaseEntity
    {
        [Display(Name = "Cevap")]
        [Required(ErrorMessage = "Cevap gereklidir.")]
        public string Answer { get; set; }
        [Display(Name = "Doğru Mu?")]
        public bool IsCorrect { get; set; }
        [Display(Name = "Soru")]
        public int QuestionId { get; set; }
        [Display(Name = "Soru")]
        public Question Question { get; set; }

    }
}

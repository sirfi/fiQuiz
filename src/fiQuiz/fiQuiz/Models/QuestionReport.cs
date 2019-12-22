using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace fiQuiz.Models
{
    public class QuestionReport : BaseEntity
    {
        [Display(Name = "Soru")]
        public int QuestionId { get; set; }
        [Display(Name = "Soru")]
        public Question Question { get; set; }
        [Display(Name = "Açıklama")]
        public string Description { get; set; }
        [Display(Name = "Kapandı Mı?")]
        public bool IsClosed { get; set; }
    }
}

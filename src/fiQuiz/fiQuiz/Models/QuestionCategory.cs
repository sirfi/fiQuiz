using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace fiQuiz.Models
{
    public class QuestionCategory:BaseEntity
    {
        [Display(Name = "Ad")]
        [Required(ErrorMessage = "Ad gereklidir.")]
        public string Name { get; set; }

        public IList<Question> Questions { get; set; }
    }
}

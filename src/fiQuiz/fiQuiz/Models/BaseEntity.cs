using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace fiQuiz.Models
{
    public interface ITrackable
    {
        [Display(Name = "Oluşturulma Zamanı")]
        DateTime CreatedAt { get; set; }
        [Display(Name = "Oluşturan Kullanıcı")]
        string CreatedBy { get; set; }
        [Display(Name = "Son Düzenlenme Zamanı")]
        DateTime LastUpdatedAt { get; set; }
        [Display(Name = "Son Düzenleyen Kullanıcı")]
        string LastUpdatedBy { get; set; }
    }

    public class BaseEntity : ITrackable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public virtual DateTime CreatedAt { get; set; }
        public virtual string CreatedBy { get; set; }
        public virtual DateTime LastUpdatedAt { get; set; }
        public virtual string LastUpdatedBy { get; set; }


    }
}

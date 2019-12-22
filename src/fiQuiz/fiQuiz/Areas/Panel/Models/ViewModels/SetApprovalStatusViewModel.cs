using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using fiQuiz.Models;

namespace fiQuiz.Areas.Panel.Models.ViewModels
{
    public class SetApprovalStatusViewModel
    {
        public int Id { get; set; }
        public QuestionApprovalStatus ApprovalStatus { get; set; }
    }
}

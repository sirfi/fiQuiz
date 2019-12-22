using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using fiQuiz.Models;

namespace fiQuiz.Areas.Panel.Models.ViewModels
{
    public class UserRolesViewModel
    {
        public ApplicationUser User { get; set; }
        public List<UserRoleStatusViewModel> UserRoleStatusList { get; set; }
    }

    public class UserRoleStatusViewModel
    {
        public ApplicationRole Role { get; set; }
        public bool Status { get; set; }
    }
}

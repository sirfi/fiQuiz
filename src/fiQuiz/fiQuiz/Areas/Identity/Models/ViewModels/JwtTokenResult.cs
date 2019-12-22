using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace fiQuiz.Areas.Identity.Models.ViewModels
{
    public class JwtTokenResult
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public string Token { get; set; }
    }
}

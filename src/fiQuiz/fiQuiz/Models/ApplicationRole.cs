﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace fiQuiz.Models
{
    public class ApplicationRole : IdentityRole
    {
        public string FullName { get; set; }
    }
}

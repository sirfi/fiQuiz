using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace fiQuiz.Core
{
    public static class EnumExtensions
    {
        public static string GetDisplayName(this Enum val)
        {
            return val.GetType()
                       .GetMember(val.ToString())
                       .FirstOrDefault()
                       ?.GetCustomAttribute<DisplayAttribute>(false)
                       ?.Name
                   ?? val.ToString();
        }
    }
}

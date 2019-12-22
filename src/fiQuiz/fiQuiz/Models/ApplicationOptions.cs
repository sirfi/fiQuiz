using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace fiQuiz.Models
{
    public class ApplicationOptions
    {
        public string ApplicationName { get; set; }
        public int QuizAnswerTime { get; set; }
        public int QuizAnswerTimeNetworkTolerance { get; set; }
        public int QuizQuestionCount { get; set; }
    }
}

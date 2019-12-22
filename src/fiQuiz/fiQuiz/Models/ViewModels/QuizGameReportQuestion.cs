using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace fiQuiz.Models.ViewModels
{
    public class QuizGameReportQuestionRequest : ApiRequestBase
    {
        public int QuizId { get; set; }
        public int QuizQuestionId { get; set; }
        public string Description { get; set; }
    }
    public class QuizGameReportQuestionResponse : ApiResponseBase
    {
        
    }

}

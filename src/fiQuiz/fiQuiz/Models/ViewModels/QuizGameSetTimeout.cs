using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace fiQuiz.Models.ViewModels
{
    public class QuizGameSetTimeoutRequest : ApiRequestBase
    {
        public int QuizId { get; set; }
    }
    public class QuizGameSetTimeoutResponse : ApiResponseBase
    {

    }
}

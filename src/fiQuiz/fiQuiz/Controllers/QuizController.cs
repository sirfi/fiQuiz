using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using fiQuiz.Areas.Identity.Controllers;
using fiQuiz.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace fiQuiz.Controllers
{
    [Authorize]
    public class QuizController : Controller
    {
        private readonly TokenService _tokenService;

        public QuizController(TokenService tokenService)
        {
            _tokenService = tokenService;
        }
        public async Task<IActionResult> Index()
        {
            ViewBag.TokenJson = JsonConvert.SerializeObject(await _tokenService.GetToken(User.Identity.Name), new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
            return View();
        }
    }
}
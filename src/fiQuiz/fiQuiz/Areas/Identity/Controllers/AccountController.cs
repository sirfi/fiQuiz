using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using fiQuiz.Areas.Identity.Models.ViewModels;
using fiQuiz.Core;
using fiQuiz.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace fiQuiz.Areas.Identity.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly TokenService _tokenService;

        public AccountController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IConfiguration configuration,
            TokenService tokenService
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
        }

        [HttpPost]
        public async Task<ActionResult<JwtTokenResult>> Login([FromBody] LoginDto model)
        {
            SignInResult result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);

            if (result.Succeeded)
            {
                ApplicationUser appUser = await _userManager.FindByNameAsync(model.Email);
                return Ok(_tokenService.GenerateJwtToken(appUser));
            }

            return BadRequest("Girdiğiniz bilgilerin doğruluğunu kontrol ediniz.");
        }

        [HttpPost]
        public async Task<ActionResult<JwtTokenResult>> Register([FromBody] RegisterDto model)
        {
            ApplicationUser user = new ApplicationUser
            {
                UserName = model.Email,
                Email = model.Email,
                FullName = model.FullName
            };
            IdentityResult result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, false);
                return Ok(_tokenService.GenerateJwtToken(user));
            }

            return BadRequest(result.Errors);
        }

        [HttpGet]
        [Authorize]
        public async Task<JwtTokenResult> GetToken()
        {
            return await _tokenService.GetToken(User.Identity.Name);
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<bool>> Logout()
        {
            await _signInManager.SignOutAsync();
            return true;
        }

        public class LoginDto
        {
            [Required(ErrorMessage = "{0} alanı gereklidir.")]
            public string Email { get; set; }

            [Required(ErrorMessage = "{0} alanı gereklidir.")]
            public string Password { get; set; }
        }

        public class RegisterDto
        {
            [Required(ErrorMessage = "{0} alanı gereklidir.")]
            public string FullName { get; set; }
            [Required(ErrorMessage = "{0} alanı gereklidir.")]
            public string Email { get; set; }

            [Required(ErrorMessage = "{0} alanı gereklidir.")]
            [StringLength(100, ErrorMessage = "PASSWORD_MIN_LENGTH", MinimumLength = 6)]
            public string Password { get; set; }
        }
    }
}
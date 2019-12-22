using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using fiQuiz.Areas.Identity.Models.ViewModels;
using fiQuiz.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace fiQuiz.Core
{
    public class TokenService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;

        public TokenService(
            UserManager<ApplicationUser> userManager,
            IConfiguration configuration
        )
        {
            _userManager = userManager;
            _configuration = configuration;
        }
        public async Task<object> GetToken(string userName)
        {
            ApplicationUser appUser = await _userManager.FindByNameAsync(userName);
            return GenerateJwtToken(appUser);
        }
        public JwtTokenResult GenerateJwtToken(ApplicationUser user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id)
            };

            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtKey"]));
            SigningCredentials credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            DateTime expires = DateTime.Now.AddDays(Convert.ToDouble(_configuration["JwtExpireDays"]));

            JwtSecurityToken token = new JwtSecurityToken(
                _configuration["JwtIssuer"],
                _configuration["JwtIssuer"],
                claims,
                expires: expires,
                signingCredentials: credentials
            );
            JwtTokenResult result = new JwtTokenResult
            {
                UserName = user.UserName,
                FullName = user.FullName,
                Email = user.Email,
                Token = new JwtSecurityTokenHandler().WriteToken(token)
            };
            return result;
        }
    }
}

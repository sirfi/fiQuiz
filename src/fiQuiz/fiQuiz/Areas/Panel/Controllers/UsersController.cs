using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using fiQuiz.Areas.Panel.Models.ViewModels;
using fiQuiz.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace fiQuiz.Areas.Panel.Controllers
{
    [Area("Panel")]
    [Authorize(Roles = "Admin")]
    public class UsersController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;

        public UsersController(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _userManager.Users.ToListAsync());
        }

        public async Task<IActionResult> Roles(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            var userRoles = await _userManager.GetRolesAsync(user);
            UserRolesViewModel viewModel = new UserRolesViewModel
            {
                User = user,
                UserRoleStatusList = _roleManager.Roles.Select(x=>new UserRoleStatusViewModel
                {
                    Role = x,
                    Status = userRoles.Contains(x.Name)
                }).ToList()
            };
            return View(viewModel);
        }

        public async Task<IActionResult> SetRole(string id, string role, bool status)
        {
            if (id == null)
            {
                return NotFound();
            }
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            if (status)
                await _userManager.AddToRoleAsync(user, role);
            else
                await _userManager.RemoveFromRoleAsync(user, role);
            return RedirectToAction("Roles", new { id });
        }
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            await _userManager.DeleteAsync(user);
            return RedirectToAction(nameof(Index));
        }
    }
}
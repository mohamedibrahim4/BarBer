using BarBerEMR.Models;
using BarBerEMR.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BarBerEMR.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UsersController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        public UsersController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<IActionResult> Index()
            {
            //var roles = await _roleManager.GetRoleIdAsync(User);
            var users = await _userManager.Users.Select(user => new UserViewModel
            {
                Id = user.Id,
                Name = user.UserName,
                Email = user.Email,
                Roles = _userManager.GetRolesAsync(user).Result
            }).ToListAsync();


            return View(users);
        }

        public async Task<IActionResult> add()
        {
            var roles = await _roleManager.Roles.Select(r=>new RoleViewModel { RoleId=r.Id,RoleName=r.Name}).ToListAsync();
          

            var ViewModel = new AddUserViewModel
            {

                Roles = roles


            };
            return View(ViewModel);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> add(AddUserViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);
            if (!model.Roles.Any(r=>r.IsSelected))
            {
                ModelState.AddModelError("Roles", "Please Select At least one role");
                return View(model);
            }
            if (await _userManager.FindByEmailAsync(model.Email) != null)
            {
                ModelState.AddModelError("Email", "Email Already Exist");
                return View(model);
            }

            if (await _userManager.FindByNameAsync(model.UserName) != null)
            {
                ModelState.AddModelError("UserName", "UserName Already Exist");
                return View(model);
            }


            var user = new ApplicationUser
            {
                UserName = model.UserName,
                Email = model.Email
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("Roles", error.Description);
                }
                return View(model);
            }


            await _userManager.AddToRolesAsync(user, model.Roles.Where(r=>r.IsSelected).Select(r => r.RoleName));


            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> ManageRoles(string userId)
        {
            var user =await _userManager.FindByIdAsync(userId);
            var roles = await _roleManager.Roles.ToListAsync();
            if (user == null)
                return NotFound();

            var ViewModel = new UserRoleViewModel
            {
                UserId = user.Id.ToString(),
                UserName = user.UserName,
                Roles= roles.Select(role => new RoleViewModel { 
                RoleId  =role.Id,
                RoleName=role.Name,
                IsSelected=_userManager.IsInRoleAsync(user,role.Name).Result
                }).ToList()

                
            };
            return View(ViewModel);
        
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ManageRoles(UserRoleViewModel model)
        {
            var user = await _userManager.FindByIdAsync(model.UserId);
            var roles = await _roleManager.Roles.ToListAsync();
            if (user == null)
                return NotFound();
            var userRoles =await _userManager.GetRolesAsync(user);
            foreach (var role in model.Roles)
            {
                if (userRoles.Any(r =>r ==role.RoleName) && !role.IsSelected)
                    await _userManager.RemoveFromRoleAsync(user, role.RoleName);
                if (!userRoles.Any(r => r == role.RoleName) && role.IsSelected)
                    await _userManager.AddToRoleAsync(user, role.RoleName);
               
            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            var roles = await _roleManager.Roles.ToListAsync();
            if (user == null)
                return NotFound();

            var ViewModel = new ProfileFormViewModel
            {
                Id = user.Id,
                UserName = user.UserName,
               Email=user.Email


            };
            return View(ViewModel);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ProfileFormViewModel model)
        {
            var user = await _userManager.FindByIdAsync(model.Id);
            var roles = await _roleManager.Roles.ToListAsync();
            if (user == null)
                return NotFound();

            var userWithSamEmail =await _userManager.FindByEmailAsync(model.Email);
            if (userWithSamEmail != null && userWithSamEmail.Id.ToString() != model.Id)
            {
                ModelState.AddModelError("Email", "This Email already take before");
                return View(model);
            }
            var userWithSamUserName =await _userManager.FindByNameAsync(model.UserName);
            if (userWithSamUserName != null && userWithSamUserName.UserName!= model.Id)
            {
                ModelState.AddModelError("UserName", "This UserName already take before");
                return View(model);
            }

            user.UserName = model.UserName;
            user.Email = model.Email;
            await _userManager.UpdateAsync(user);

            return RedirectToAction(nameof(Index));
        }
    }
}

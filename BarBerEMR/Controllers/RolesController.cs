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
    public class RolesController : Controller
    {
        public RoleManager<IdentityRole> _roleManager { get; set; }
        public RolesController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }
        public async Task<IActionResult>  Index()
        {
          var roles=  await _roleManager.Roles.ToListAsync();
            return View(roles);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> add(RoleFormViewModel role)
        {
            if (! ModelState.IsValid)
                return View("Index", await _roleManager.Roles.ToListAsync());
            if (await _roleManager.RoleExistsAsync(role.Name))
            {
                ModelState.AddModelError("Name", "Role Is Exist");
                return View("Index", await _roleManager.Roles.ToListAsync());
            }
           await _roleManager.CreateAsync(new IdentityRole(role.Name));
            return RedirectToAction(nameof(Index));
        }
    }
}

using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using RVASIspit.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace RVASIspit.Controllers
{
    public class RoleController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public RoleController()
        {
            var context = new ApplicationDbContext();
            _userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            _roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
        }

        [HttpPost]
        public async Task<ActionResult> AddUserToRole(string userId, string roleName)
        {
            if (await _roleManager.RoleExistsAsync(roleName))
            {
                var result = await _userManager.AddToRoleAsync(userId, roleName);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    // Handle errors here
                    return View("Error");
                }
            }
            else
            {
                // Handle case where role does not exist
                return View("Error");
            }
        }
    }
}
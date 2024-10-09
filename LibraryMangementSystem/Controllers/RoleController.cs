using LibraryMangementSystem.Models;
using LibraryMangementSystem.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace LibraryMangementSystem.Controllers
{
    [Authorize(Roles ="Admin")]
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<ApplicationUser> userManager;

        public RoleController(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
        }
        public async Task<IActionResult> GetAllUsers()
        {
            List<ApplicationUser> users =  userManager.Users.ToList();
            List<UsersAndRolesViewModel> usersAndRolesViewModels = new List<UsersAndRolesViewModel>();
            foreach (var user in users)
            {
                var roles = await userManager.GetRolesAsync(user);
                usersAndRolesViewModels.Add(new UsersAndRolesViewModel { UserName = user.UserName, Roles = roles });
            }
            return View("GetAllUsers", usersAndRolesViewModels);
        }

        public async Task<IActionResult> AssignToRole(string UserName)
        {
            var user = await userManager.FindByNameAsync(UserName);
            var roles = roleManager.Roles.ToList();
            var userRoles = await userManager.GetRolesAsync(user);

            AssignViewModel model = new AssignViewModel
            {
                UserName = user.UserName,
                Roles = roles.Select(role => new RoleSelection
                {
                    RoleName = role.Name
                }).ToList(),
                AssignedRoles = userRoles.ToList()
            };
            return View("AssignToRole", model);
        }

        public async Task<IActionResult> SaveAssign(AssignViewModel model)
        {
            ApplicationUser user = await userManager.FindByNameAsync(model.UserName);
           

            var userRoles = await userManager.GetRolesAsync(user);
            var rolesToAdd = model.AssignedRoles.Except(userRoles);
            var rolesToRemove = userRoles.Except(model.AssignedRoles);

            await userManager.AddToRolesAsync(user, rolesToAdd);
            await userManager.RemoveFromRolesAsync(user, rolesToRemove);

            return RedirectToAction("GetAllUsers");
        }

        public IActionResult AddRole()
        {
            return View("AddRole");
        }

        public async Task<IActionResult> SaveRole(RoleViewModel roleViewModel)
        {
            if(ModelState.IsValid)
            {
                IdentityRole role = new IdentityRole();
                role.Name = roleViewModel.RoleName;

                IdentityResult result = await roleManager.CreateAsync(role);
                if (result.Succeeded)
                {
                    return View("AddRole");
                }
                
                foreach(var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View("AddRole");
        }

    }
}

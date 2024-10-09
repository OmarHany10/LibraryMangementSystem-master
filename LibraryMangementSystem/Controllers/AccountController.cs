using LibraryMangementSystem.Models;
using LibraryMangementSystem.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LibraryMangementSystem.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }
        public IActionResult Register()
        {
            return View("Register");
        }

        public async Task<IActionResult> SaveRegister(RegisterViewModel registerViewModel)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser applicationUser = new ApplicationUser();
                applicationUser.UserName = registerViewModel.UserName;
                applicationUser.Address = registerViewModel.Address;
                applicationUser.PasswordHash = registerViewModel.Password;

                IdentityResult result = await userManager.CreateAsync(applicationUser, registerViewModel.Password);
                if (result.Succeeded)  
                {
                    // assign to role
                    await userManager.AddToRoleAsync(applicationUser, "Members");

                    await signInManager.SignInAsync(applicationUser, isPersistent: false);
                    return RedirectToAction("Index", "Home");
                }
                foreach(var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);  
                }
            }
            return View("Register", registerViewModel);
        }

        public IActionResult Login()
        {
            return View("Login");
        }

        public async Task<IActionResult> SaveLogin(LoginViewModel loginViewModel)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser applicationUser = await userManager.FindByNameAsync(loginViewModel.UserName);

                if (applicationUser != null)
                {
                    bool found = await userManager.CheckPasswordAsync(applicationUser, loginViewModel.Password);

                    if (found)
                    {
                        await signInManager.SignInAsync(applicationUser, loginViewModel.RememberMe);
                        return RedirectToAction("Index", "Home");
                    }
                }
                ModelState.AddModelError("", "Incorrext Username or Password");
            }
            return View("Login", loginViewModel);
        }

        public async Task<IActionResult> SignOut()
        {
            await signInManager.SignOutAsync();
            return View("Login");
        }
    }
}

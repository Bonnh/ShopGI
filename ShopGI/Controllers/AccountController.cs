using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ShopGI.Models.ViewModels;
using System.Net.Mail;
using MailKit.Net.Smtp;
using System.Security.Claims;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;

namespace ShopGI.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                
                var user = new IdentityUser
                {
                    NormalizedUserName = model.Usernamee,
                    UserName = model.Usernamee,
                    Email = model.Email,
                };

                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    //không cần xác thực email
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", "Home");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (!ModelState.IsValid) return View(loginViewModel);

            var user = await _userManager.FindByNameAsync(loginViewModel.Usernamee);
            if (user != null)
            {
                var passwordCheck = await _userManager.CheckPasswordAsync(user, loginViewModel.Password);
                if (passwordCheck)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, loginViewModel.Password, false, false);
                    if (result.Succeeded)
                    {
                        //if (loginViewModel.RememberMe)
                        //{
                        //}
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        TempData["Error"] = "Wrong credentials. Please, try again!";
                        return View(loginViewModel);
                    }
                }
                else
                {
                    TempData["Error"] = "Wrong credentials. Please, try again!";
                    return View(loginViewModel);
                }
            }
            else
            {
                TempData["Error"] = "Wrong credentials. Please, try again!";
                return View(loginViewModel);
            }

            
        }
        public async Task<IActionResult> Logout(LoginViewModel loginViewModel)
        {
            _signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }
        public IActionResult Manager()
        {
            return View();
        }
    }
}

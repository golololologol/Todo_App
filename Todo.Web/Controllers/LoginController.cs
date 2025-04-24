using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Todo.Web.Clients.Interfaces;
using Todo.Web.Clients.Models;
using Todo.Web.Models;

namespace Todo.Web.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUserClient _userClient;

        public LoginController(IUserClient userClient)
        {
            _userClient = userClient;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([Bind("Name, Password")] LoginViewModel model)
        {
            var userId = await _userClient.ValidatePassword(new Clients.Models.ValidatePasswordInputModel
            {
                Name = model.Name!,
                Password = model.Password!
            });

            if (userId is null || userId <= 0)
            {
                ModelState.AddModelError("Password", "Invalid password"); 
                return View(model);
            }

            var user = await _userClient.GetById(userId);
            if (user is null)
            {
                ModelState.AddModelError("Password", "Invalid password");
                return View(model);
            }

            await SignInAsync(user.Id, model.Name!, user.IsAdmin);

            return RedirectToAction(
                nameof(HomeController.Index), 
                "Home");
        }

        [HttpGet]
        public IActionResult Register()
        {
            ViewBag.TaskList = 12341;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (model.Password != model.RepeatPassword)
            {
                ModelState.TryAddModelError(
                    nameof(model.RepeatPassword), 
                    "Passwords don't match");

                return View(model);
            }

            try
            {
                var userId = await _userClient.CreateUser(new Clients.Models.CreateUserInputModel
                {
                    IsAdmin = false,
                    Name = model.Name!,
                    Password = model.Password!
                });

                await SignInAsync(userId, model.Name!, false);
            }
            catch(Exception ex)
            {
                Console.WriteLine($"An error occurred during registration: {ex.Message}");
            
                ModelState.TryAddModelError(
                    string.Empty,
                    "An error occurred during registration.");
            
                return View(model);
            }

            return RedirectToAction(
                nameof(HomeController.Index),
                "Home");
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            var user = await _userClient.GetById(
                int.Parse(HttpContext.User.FindFirst("userId")?.Value ?? string.Empty));

            await HttpContext.SignOutAsync();
            return RedirectToAction(nameof(Index));
        }

        private async Task SignInAsync(int? userId, string userName, bool isAdmin)
        {
            var claimsIdentity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme, "name", "role");
            claimsIdentity.AddClaim(new Claim("name", userName));
            claimsIdentity.AddClaim(new Claim("userId", userId?.ToString() ?? string.Empty));
            claimsIdentity.AddClaim(new Claim("role", isAdmin ? "Administrator" : "User"));

            var principal = new ClaimsPrincipal(claimsIdentity);

            await HttpContext.SignInAsync(principal);
        }
    }
}

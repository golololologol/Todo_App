using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Todo.Web.Clients.Interfaces; // Corrected namespace
using Todo.Web.Models;

namespace Todo.Web.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class UsersController : Controller
    {
        private readonly IUserClient _userClient;

        public UsersController(IUserClient userClient)
        {
            _userClient = userClient;
        }

        public async Task<IActionResult> Index()
        {
            var users = await _userClient.GetAll();

            var viewModels = users
                .Select(e => new UserViewModel { Id = e.Id, Name = e.Name, IsAdmin = e.IsAdmin })
                .ToArray();

            return View(viewModels);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _userClient.GetById(id);

            if (user == null)
            {
                return NotFound();
            }

            return View(new UserViewModel
            {
                Id = user.Id,
                Name = user.Name,
                IsAdmin = user.IsAdmin
            });
        }

        public IActionResult Create()
        {
            return View();
        }
                
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name, Password, IsAdmin")] CreateUserViewModel userViewModel)
        {
            if (ModelState.IsValid)
            {
                await _userClient.CreateUser(new Clients.Models.CreateUserInputModel
                {
                    Name = userViewModel.Name!,
                    Password = userViewModel.Password!,
                    IsAdmin = userViewModel.IsAdmin
                });

                return RedirectToAction(nameof(Index));
            }

            return View(userViewModel);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _userClient.GetById(id);

            if (user == null)
            {
                return NotFound();
            }

            return View(new UserViewModel
            {
                Id = user.Id,
                Name = user.Name,
                IsAdmin = user.IsAdmin
            });
        }
                
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, [Bind("Id,Name")] UserViewModel userViewModel)
        {
            if (id != userViewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _userClient.UpdateUser(id, new Clients.Models.UpdateUserInputModel
                {
                    Name = userViewModel.Name
                });

                return RedirectToAction(nameof(Index));
            }

            return View(userViewModel);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _userClient.GetById(id);

            if (user == null)
            {
                return NotFound();
            }

            return View(new UserViewModel
            {
                Id = user.Id,
                Name = user.Name
            });
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            await _userClient.DeleteUser(id);

            return RedirectToAction(nameof(Index));
        }
    }
}

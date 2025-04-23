using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Todo.Web.Clients.Interfaces;
using Todo.Web.Clients.Models;
using Todo.Web.Models;

namespace Todo.Web.Controllers
{
    [Authorize]
    public class TodoListsController : Controller
    {
        private readonly ITodoListClient _todoListClient;

        public TodoListsController(ITodoListClient todoListClient)
        {
            _todoListClient = todoListClient;
        }

        public async Task<IActionResult> Index()
        {
            var lists = await _todoListClient.GetAll();
            var viewModels = lists.Select(l => new TodoListViewModel
            {
                Id = l.Id,
                Description = l.Description,
                IsActive = l.IsActive,
                Date = l.Date,
                NumberOfTasks = l.NumberOfTasks
            });
            return View(viewModels);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var list = await _todoListClient.GetById(id);
            if (list == null) return NotFound();

            var vm = new TodoListViewModel
            {
                Id = list.Id,
                Description = list.Description,
                IsActive = list.IsActive,
                Date = list.Date,
                NumberOfTasks = list.NumberOfTasks
            };
            return View(vm);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateTodoListViewModel model)
        {
            if (ModelState.IsValid)
            {
                int? userId = null;
                var claim = User.FindFirst("userId");
                if (claim != null && int.TryParse(claim.Value, out var uid)) userId = uid;

                await _todoListClient.CreateTodoList(new CreateTodoListInputModel
                {
                    Description = model.Description,
                    UserId = userId
                });

                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var list = await _todoListClient.GetById(id);
            if (list == null) return NotFound();

            var vm = new TodoListViewModel
            {
                Id = list.Id,
                Description = list.Description,
                IsActive = list.IsActive,
                Date = list.Date,
                NumberOfTasks = list.NumberOfTasks
            };
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, TodoListViewModel model)
        {
            if (id != model.Id) return NotFound();

            if (ModelState.IsValid)
            {
                await _todoListClient.UpdateTodoList(id, new UpdateTodoListInputModel
                {
                    Description = model.Description!,
                    IsActive = model.IsActive
                });
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var list = await _todoListClient.GetById(id);
            if (list == null) return NotFound();

            var vm = new TodoListViewModel
            {
                Id = list.Id,
                Description = list.Description,
                IsActive = list.IsActive,
                Date = list.Date,
                NumberOfTasks = list.NumberOfTasks
            };
            return View(vm);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            await _todoListClient.DeleteTodoList(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
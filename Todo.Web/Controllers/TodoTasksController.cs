using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Todo.Web.Clients.Interfaces;
using Todo.Web.Clients.Models;
using Todo.Web.Models;

namespace Todo.Web.Controllers
{
    [Authorize]
    public class TodoTasksController : Controller
    {
        private readonly ITodoTaskClient _taskClient;
        private readonly ITodoListClient _listClient;

        public TodoTasksController(ITodoTaskClient taskClient, ITodoListClient listClient)
        {
            _taskClient = taskClient;
            _listClient = listClient;
        }

        public async Task<IActionResult> Index()
        {
            var tasks = await _taskClient.GetAll();
            var viewModels = tasks.Select(t => new TodoTaskViewModel
            {
                Id = t.Id,
                TodoId = t.TodoId,
                Description = t.Description,
                IsCompleted = t.IsCompleted,
                DueDate = t.DueDate
            });
            return View(viewModels);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();
            var task = await _taskClient.GetById(id);
            if (task == null) return NotFound();
            var vm = new TodoTaskViewModel
            {
                Id = task.Id,
                TodoId = task.TodoId,
                Description = task.Description,
                IsCompleted = task.IsCompleted,
                DueDate = task.DueDate
            };
            return View(vm);
        }

        public async Task<IActionResult> Create()
        {
            var lists = await _listClient.GetAll();
            ViewBag.Lists = new SelectList(lists, "Id", "Description");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateTodoTaskViewModel model)
        {
            if (ModelState.IsValid)
            {
                int? ownerId = null;
                var claim = User.FindFirst("userId");
                if (claim != null && int.TryParse(claim.Value, out var uid)) ownerId = uid;

                await _taskClient.CreateTodoTask(new CreateTodoTaskInputModel
                {
                    OwnerId = ownerId,
                    TodoId = model.TodoId,
                    Description = model.Description!,
                    DueDate = model.DueDate
                });
                return RedirectToAction(nameof(Index));
            }
            var lists = await _listClient.GetAll();
            ViewBag.Lists = new SelectList(lists, "Id", "Description", model.TodoId);
            return View(model);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var task = await _taskClient.GetById(id);
            if (task == null) return NotFound();
            var vm = new TodoTaskViewModel
            {
                Id = task.Id,
                TodoId = task.TodoId,
                Description = task.Description,
                IsCompleted = task.IsCompleted,
                DueDate = task.DueDate
            };
            var lists = await _listClient.GetAll();
            ViewBag.Lists = new SelectList(lists, "Id", "Description", vm.TodoId);
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, TodoTaskViewModel model)
        {
            if (id != model.Id) return NotFound();
            if (ModelState.IsValid)
            {
                await _taskClient.UpdateTodoTask(id, new UpdateTodoTaskInputModel
                {
                    Description = model.Description!,
                    IsCompleted = model.IsCompleted,
                    DueDate = model.DueDate
                });
                return RedirectToAction(nameof(Index));
            }
            var lists = await _listClient.GetAll();
            ViewBag.Lists = new SelectList(lists, "Id", "Description", model.TodoId);
            return View(model);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var task = await _taskClient.GetById(id);
            if (task == null) return NotFound();
            var vm = new TodoTaskViewModel
            {
                Id = task.Id,
                TodoId = task.TodoId,
                Description = task.Description,
                IsCompleted = task.IsCompleted,
                DueDate = task.DueDate
            };
            return View(vm);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            await _taskClient.DeleteTodoTask(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
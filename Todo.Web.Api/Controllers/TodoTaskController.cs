using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Todo.Domain.Services;
using Todo.Web.Api.Models;

namespace Todo.Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoTaskController : ControllerBase
    {
        private readonly ITodoTaskService _todoTaskService;

        public TodoTaskController(ITodoTaskService todoTaskService)
        {
            _todoTaskService = todoTaskService;
        }

        [HttpGet("GetById/{id}")]
        public IActionResult GetById([FromRoute, Required] int? id)
        {
            if (id == null)
                return BadRequest("task id was missing");

            var task = _todoTaskService.GetTodoTask(id.Value);
            if (task == null)
                return BadRequest("todo task not found");

            return Ok(new { task.Id, task.Description, task.IsCompleted, task.DueDate, task.TodoId });
        }

        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            var tasks = _todoTaskService.GetTodoTasks();
            return Ok(tasks.Select(t => new { t.Id, t.Description, t.IsCompleted, t.DueDate, t.TodoId }).ToArray());
        }

        [HttpPost("CreateTodoTask")]
        public IActionResult CreateTodoTask([FromBody] CreateTodoTaskInput input)
        {
            if (input.TodoId == null || string.IsNullOrEmpty(input.Description))
                return BadRequest("Invalid input parameters");

            _todoTaskService.Create(input.TodoId.Value, input.Description, input.DueDate);
            return Ok();
        }

        [HttpPut("UpdateTodoTask/{id}")]
        public IActionResult UpdateTodoTask([FromRoute] int? id, [FromBody] UpdateTodoTaskInput input)
        {
            if (id == null || string.IsNullOrEmpty(input.Description))
                return BadRequest("Invalid input parameters");

            _todoTaskService.Update(id.Value, input.Description, input.IsCompleted, input.DueDate);
            return Ok();
        }

        [HttpDelete("DeleteTodoTask/{id}")]
        public IActionResult DeleteTodoTask([FromRoute] int? id)
        {
            if (id == null)
                return BadRequest("Invalid id provided");

            _todoTaskService.Delete(id.Value);
            return Ok();
        }
    }
}
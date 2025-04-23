using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Todo.Domain.Services;
using Todo.Web.Api.Models;

namespace Todo.Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoListController : ControllerBase
    {
        private readonly ITodoListService _todoListService;

        public TodoListController(ITodoListService todoListService)
        {
            _todoListService = todoListService;
        }

        [HttpGet("GetById/{id}")]
        public IActionResult GetById([FromRoute, Required] int? id)
        {
            if (id == null)
                return BadRequest("list id was missing");

            var list = _todoListService.GetTodoList(id.Value);
            if (list == null)
                return BadRequest("todo list not found");

            return Ok(new { list.Id, list.Description, list.IsActive, list.Date, list.NumberOfTasks, list.UserId });
        }

        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            var lists = _todoListService.GetTodoLists();
            return Ok(lists.Select(l => new { l.Id, l.Description, l.IsActive, l.Date, l.NumberOfTasks, l.UserId }).ToArray());
        }

        [HttpPost("CreateTodoList")]
        public IActionResult CreateTodoList([FromBody] CreateTodoListInput input)
        {
            if (input.UserId == null || string.IsNullOrEmpty(input.Description))
                return BadRequest("Invalid input parameters");

            _todoListService.Create(input.UserId, input.Description);
            return Ok();
        }

        [HttpPut("UpdateTodoList/{id}")]
        public IActionResult UpdateTodoList([FromRoute] int? id, [FromBody] UpdateTodoListInput input)
        {
            if (id == null || string.IsNullOrEmpty(input.Description))
                return BadRequest("Invalid input parameters");

            _todoListService.Update(id.Value, input.Description, input.IsActive);
            return Ok();
        }

        [HttpDelete("DeleteTodoList/{id}")]
        public IActionResult DeleteTodoList([FromRoute] int? id)
        {
            if (id == null)
                return BadRequest("Invalid id provided");

            _todoListService.Delete(id.Value);
            return Ok();
        }
    }
}
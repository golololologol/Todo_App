using Refit;
using System.ComponentModel.DataAnnotations;
using Todo.Web.Clients.Models;

namespace Todo.Web.Clients.Interfaces
{
    public interface ITodoListClient
    {
        [Get("/GetById/{id}")]
        Task<TodoListModel?> GetById([Required] int? id);

        [Get("/GetAll")]
        Task<TodoListModel[]> GetAll();

        [Post("/CreateTodoList")]
        Task CreateTodoList([Body] CreateTodoListInputModel input);

        [Put("/UpdateTodoList/{id}")]
        Task UpdateTodoList([Required] int? id, [Body] UpdateTodoListInputModel input);

        [Delete("/DeleteTodoList/{id}")]
        Task DeleteTodoList([Required] int? id);
    }
}
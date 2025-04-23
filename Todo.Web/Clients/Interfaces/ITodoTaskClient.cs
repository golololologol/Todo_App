using Refit;
using System.ComponentModel.DataAnnotations;
using Todo.Web.Clients.Models;

namespace Todo.Web.Clients.Interfaces
{
    public interface ITodoTaskClient
    {
        [Get("/GetById/{id}")]
        Task<TodoTaskModel?> GetById([Required] int? id);

        [Get("/GetAll")]
        Task<TodoTaskModel[]> GetAll();

        [Post("/CreateTodoTask")]
        Task CreateTodoTask([Body] CreateTodoTaskInputModel input);

        [Put("/UpdateTodoTask/{id}")]
        Task UpdateTodoTask([Required] int? id, [Body] UpdateTodoTaskInputModel input);

        [Delete("/DeleteTodoTask/{id}")]
        Task DeleteTodoTask([Required] int? id);
    }
}
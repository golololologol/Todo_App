using Refit;
using System.ComponentModel.DataAnnotations;
using Todo.Web.Clients.Models;

namespace Todo.Web.Clients.Interfaces
{
    public interface IUserClient
    {
        [Get("/GetById/{id}")]
        Task<UserModel?> GetById([Required] int? id);

        [Get("/GetAll")]
        Task<UserModel[]> GetAll();

        [Post("/CreateUser")]
        Task<int?> CreateUser([Body] CreateUserInputModel user);

        [Put("/UpdateUser/{id}")]
        Task UpdateUser([Required] int? id, [Body] UpdateUserInputModel user);

        [Delete("/DeleteUser/{id}")]
        Task DeleteUser([Required] int? id);

        [Post("/ValidatePassword")]
        Task<int?> ValidatePassword([Body] ValidatePasswordInputModel validatePassword);
    }
}

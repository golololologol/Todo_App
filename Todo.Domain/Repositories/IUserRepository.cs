using Todo.Domain.Models;

namespace Todo.Domain.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        bool IsExistingName(string name);
        User? GetByName(string name);
    }
}

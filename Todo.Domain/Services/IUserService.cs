using Todo.Domain.Models;

namespace Todo.Domain.Services
{
    public interface IUserService
    {
        public User? GetUser(int id);

        public IEnumerable<User> GetUsers();

        public int? Create(string name, string password, bool isAdmin);

        public void Update(int id, string name);

        public void Delete(int id);

        int? ValidatePassword(string name, string password);
    }
}

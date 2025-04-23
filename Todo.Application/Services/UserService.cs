using Todo.Domain.Models;
using Todo.Domain.Repositories;
using Todo.Domain.Services;

namespace Todo.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;

        public UserService(IUserRepository repository) 
        {
            _repository = repository;
        }

        public int? Create(string name, string password, bool isAdmin)
        {
            var existingUser = _repository.IsExistingName(name);
            if (existingUser)
            {
                throw new InvalidProgramException(
                    "User with such name already exists");
            }

            var hashed = HashPassword(password);
            var userId = _repository.Create(new User { Name = name, Password = hashed, IsAdmin = isAdmin });
            return userId;
        }

        public void Delete(int id)
        {
            var user = _repository.GetById(id);
            if (user is null)
            {
                throw new InvalidProgramException(
                    "User with such id does not exist");
            }

            _repository.Delete(user);
        }

        public User? GetUser(int id)
        {
            var user = _repository.GetById(id);
            return user;
        }

        public IEnumerable<User> GetUsers()
        {
            var user = _repository.GetAll();
            return user;
        }

        public void Update(int id, string name)
        {
            var user = _repository.GetById(id);
            if (user is null)
            {
                throw new InvalidProgramException(
                    "User with such id does not exist");
            }

            if (user.Name == name)
            {
                return;
            }

            var isExistingUsername = _repository.IsExistingName(name);
            if (isExistingUsername)
            {
                throw new InvalidProgramException(
                    "User with such name already exists");
            }

            user.Name = name;

            _repository.Update(user);
        }

        public int? ValidatePassword(string name, string password)
        {
            var user = _repository.GetByName(name);
            if (user is null)
            {
                return null;
            }

            if (user.Password != HashPassword(password))
            {
                return null;
            }

            return user.Id;
        }

        private static string HashPassword(string password)
        {
            using var sha = System.Security.Cryptography.SHA256.Create();
            var bytes = System.Text.Encoding.UTF8.GetBytes(password);
            var hash = sha.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }
    }
}

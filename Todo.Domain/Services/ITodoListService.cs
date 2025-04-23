using Todo.Domain.Models;

namespace Todo.Domain.Services
{
    public interface ITodoListService
    {
        void Create(int? ownerId, string description);
        void Delete(int id);
        TodoList? GetTodoList(int id);
        IEnumerable<TodoList> GetTodoLists();
        void Update(int id, string description, bool isActive);
    }
}
using Todo.Domain.Models;

namespace Todo.Domain.Services
{
    public interface ITodoTaskService
    {
        void Create(int holderId, string description, DateTime dueDate);
        void Delete(int id);
        TodoTask? GetTodoTask(int id);
        IEnumerable<TodoTask> GetTodoTasks();
        void Update(int id, string description, bool isCompleted, DateTime dueDate);
    }
}
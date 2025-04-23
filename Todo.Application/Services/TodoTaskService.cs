using Todo.Domain.Models;
using Todo.Domain.Repositories;
using Todo.Domain.Services;

namespace Todo.Application.Services
{
    public class TodoTaskService : ITodoTaskService
    {
        private readonly ITodoListRepository _todoListRepository;
        private readonly IUserRepository _userRepository;
        private readonly ITodoTaskRepository _repository;

        public TodoTaskService(
            ITodoListRepository todoListRepository,
            IUserRepository userRepository,
            ITodoTaskRepository repository)
        {
            _todoListRepository = todoListRepository;
            _userRepository = userRepository;
            _repository = repository;
        }

        public void Create(int holderId, string description, DateTime dueDate)
        {
            var todoList = _todoListRepository.GetById(holderId);
            if (todoList is null)
                throw new InvalidProgramException("TodoList with such id does not exist");

            var task = new TodoTask
            {
                Description = description,
                DueDate = dueDate,
                Holder = todoList,
                IsCompleted = false,
                TodoId = holderId
            };
            _repository.Create(task);

            todoList.NumberOfTasks++;
            _todoListRepository.Update(todoList);
        }

        public void Delete(int id)
        {
            var todoTask = _repository.GetById(id);
            if (todoTask is null)
                throw new InvalidProgramException("TodoTask with such id does not exist");

            var todoList = todoTask.Holder;
            _repository.Delete(todoTask);
            if (todoList != null)
            {
                todoList.NumberOfTasks = Math.Max(0, todoList.NumberOfTasks - 1);
                _todoListRepository.Update(todoList);
            }
        }

        public TodoTask? GetTodoTask(int id)
        {
            var todoTask = _repository.GetById(id);
            return todoTask;
        }

        public IEnumerable<TodoTask> GetTodoTasks()
        {
            var todoTasks = _repository.GetAll();
            return todoTasks;
        }

        public void Update(
            int id,
            string description,
            bool isCompleted,
            DateTime dueDate)
        {
            var todoTask = _repository.GetById(id);
            if (todoTask is null)
            {
                throw new InvalidProgramException(
                    "TodoTask with such id does not exist");
            }

            todoTask.Description = description;
            todoTask.IsCompleted = isCompleted;
            todoTask.DueDate = dueDate;

            _repository.Update(todoTask);
        }
    }
}

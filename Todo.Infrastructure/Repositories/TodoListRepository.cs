using Todo.Domain.Models;
using Todo.Domain.Repositories;

namespace Todo.Infrastructure.Repositories
{
    internal class TodoListRepository : ITodoListRepository
    {
        private readonly DatabaseContext _databaseContext;

        public TodoListRepository(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public int? Create(TodoList entity)
        {
            _databaseContext.Set<TodoList>().Add(entity);
            return entity.Id;
        }

        public void Delete(TodoList entity)
        {
            _databaseContext.Set<TodoList>().Remove(entity);
        }

        public IEnumerable<TodoList> GetAll()
        {
            return _databaseContext.Set<TodoList>().AsQueryable().ToList();
        }

        public TodoList? GetById(int id)
        {
            return _databaseContext.Set<TodoList>().AsQueryable().Where(e => e.Id == id).FirstOrDefault();
        }

        public void Update(TodoList entity)
        {
            _databaseContext.Update<TodoList>(entity);
        }
    }
}

using Todo.Domain.Models;
using Todo.Domain.Repositories;

namespace Todo.Infrastructure.Repositories
{
    internal class TodoTaskRepository : ITodoTaskRepository
    {
        private readonly DatabaseContext _databaseContext;

        public TodoTaskRepository(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public int? Create(TodoTask entity)
        {
            _databaseContext.Set<TodoTask>().Add(entity);
            return entity.Id;
        }

        public void Delete(TodoTask entity)
        {
            _databaseContext.Set<TodoTask>().Remove(entity);
        }

        public IEnumerable<TodoTask> GetAll()
        {
            return _databaseContext.Set<TodoTask>().AsQueryable().ToList();
        }

        public TodoTask? GetById(int id)
        {
            return _databaseContext.Set<TodoTask>().AsQueryable().Where(e => e.Id == id).FirstOrDefault();
        }

        public void Update(TodoTask entity)
        {
            _databaseContext.Update<TodoTask>(entity);
        }

    }
}

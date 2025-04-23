namespace Todo.Domain.Models
{
    public class User
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
        public string? Password { get; set; }
        public bool IsAdmin { get; set; }
        public virtual List<TodoList> Todos { get; set; } = new();
    }
}

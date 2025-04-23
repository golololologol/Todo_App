namespace Todo.Web.Clients.Models
{
    public class CreateTodoListInputModel
    {
        public int? UserId { get; set; }
        public string Description { get; set; } = string.Empty;
    }
}
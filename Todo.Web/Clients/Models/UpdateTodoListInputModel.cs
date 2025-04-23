namespace Todo.Web.Clients.Models
{
    public class UpdateTodoListInputModel
    {
        public string Description { get; set; } = string.Empty;
        public bool IsActive { get; set; }
    }
}
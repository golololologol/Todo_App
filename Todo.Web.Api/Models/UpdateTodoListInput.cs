namespace Todo.Web.Api.Models
{
    public class UpdateTodoListInput
    {
        public string Description { get; set; } = string.Empty;
        public bool IsActive { get; set; }
    }
}
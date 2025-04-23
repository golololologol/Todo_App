namespace Todo.Web.Api.Models
{
    public class CreateTodoListInput
    {
        public int? UserId { get; set; }
        public string Description { get; set; } = string.Empty;
    }
}
namespace Todo.Web.Api.Models
{
    public class CreateUserInput
    {
        public string Name { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public bool IsAdmin { get; set; }
    }
}

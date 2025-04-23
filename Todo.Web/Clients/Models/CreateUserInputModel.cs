namespace Todo.Web.Clients.Models
{
    public class CreateUserInputModel
    {
        public string Name { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public bool IsAdmin { get; set; }
    }
}

namespace Todo.Web.Models
{
    public class UserViewModel
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
        public bool IsAdmin { get; set; }
    }

    public class CreateUserViewModel
    {
        public string? Name { get; set; }
        public string? Password { get; set; }
        public bool IsAdmin { get; set; }
    }
}

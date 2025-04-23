using System;

namespace Todo.Web.Clients.Models
{
    public class CreateTodoTaskInputModel
    {
        public int? OwnerId { get; set; }
        public int? TodoId { get; set; }
        public string Description { get; set; } = string.Empty;
        public DateTime DueDate { get; set; }
    }
}
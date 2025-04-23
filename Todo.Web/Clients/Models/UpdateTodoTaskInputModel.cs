using System;

namespace Todo.Web.Clients.Models
{
    public class UpdateTodoTaskInputModel
    {
        public string Description { get; set; } = string.Empty;
        public bool IsCompleted { get; set; }
        public DateTime DueDate { get; set; }
    }
}
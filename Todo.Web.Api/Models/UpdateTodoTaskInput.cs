using System;

namespace Todo.Web.Api.Models
{
    public class UpdateTodoTaskInput
    {
        public string Description { get; set; } = string.Empty;
        public bool IsCompleted { get; set; }
        public DateTime DueDate { get; set; }
    }
}
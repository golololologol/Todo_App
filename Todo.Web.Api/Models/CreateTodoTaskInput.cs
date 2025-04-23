using System;

namespace Todo.Web.Api.Models
{
    public class CreateTodoTaskInput
    {
        public int? OwnerId { get; set; }
        public int? TodoId { get; set; }
        public string Description { get; set; } = string.Empty;
        public DateTime DueDate { get; set; }
    }
}
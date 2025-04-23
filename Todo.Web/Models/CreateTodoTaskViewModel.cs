using System;

namespace Todo.Web.Models
{
    public class CreateTodoTaskViewModel
    {
        public int? TodoId { get; set; }
        public string? Description { get; set; }
        public DateTime DueDate { get; set; }
    }
}
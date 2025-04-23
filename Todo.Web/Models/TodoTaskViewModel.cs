using System;

namespace Todo.Web.Models
{
    public class TodoTaskViewModel
    {
        public int? Id { get; set; }
        public int? TodoId { get; set; }
        public string? Description { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime DueDate { get; set; }
    }
}
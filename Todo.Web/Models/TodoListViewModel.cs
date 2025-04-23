using System;

namespace Todo.Web.Models
{
    public class TodoListViewModel
    {
        public int? Id { get; set; }
        public string? Description { get; set; }
        public bool IsActive { get; set; }
        public DateTime Date { get; set; }
        public int NumberOfTasks { get; set; }
    }
}
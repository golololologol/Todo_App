using System;

namespace Todo.Web.Clients.Models
{
    public class TodoListModel
    {
        public int? Id { get; set; }
        public string? Description { get; set; }
        public int? UserId { get; set; }
        public bool IsActive { get; set; }
        public DateTime Date { get; set; }
        public int NumberOfTasks { get; set; }
    }
}
using System;

namespace TodoProApp
{
    public enum TodoStatus
    {
        Pending,
        Completed
    }
    
    public class Todo
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public string Title;

        public TodoStatus Status = TodoStatus.Pending;
    }
}
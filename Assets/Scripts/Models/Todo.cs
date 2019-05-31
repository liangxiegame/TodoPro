using System;
using System.Collections.Generic;

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

        public DueDate DueDate;

        public Priority Priority = Priority.Priority4;

        public List<string> Labels = new List<string>();

        public string ProjectId { get; set; } = "1";
    }
}
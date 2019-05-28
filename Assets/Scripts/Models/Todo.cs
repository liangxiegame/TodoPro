using System;
using System.Collections.Generic;

namespace TodoProApp
{
    public enum TodoStatus
    {
        Pending,
        Completed
    }

    public enum DueDate
    {
        None,
        Today,
        Next7Day
    }

    public static class DueDateToText
    {
        public static string ToText(this DueDate dueDate)
        {
            if (dueDate == DueDate.None)
            {
                return "None";
            }  else if (dueDate == DueDate.Today)
            {
                return "Today";
            }
            else if (dueDate == DueDate.Next7Day)
            {
                return "Next 7 Day";
            }

            return "";
        }
    }
    
    public class Todo
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public string Title;

        public TodoStatus Status = TodoStatus.Pending;

        public DueDate DueDate;
        
        public List<string> Labels = new List<string>();
    }
}
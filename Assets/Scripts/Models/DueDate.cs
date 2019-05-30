namespace TodoProApp
{
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
            }
            else if (dueDate == DueDate.Today)
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
}
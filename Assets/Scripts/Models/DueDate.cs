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
        
        public static string ToTitle(this DueDate dueDate)
        {
            if (dueDate == DueDate.None)
            {
                return "无";
            }
            else if (dueDate == DueDate.Today)
            {
                return "今天";
            }
            else if (dueDate == DueDate.Next7Day)
            {
                return "一周内";
            }

            return "";
        }
    }
}
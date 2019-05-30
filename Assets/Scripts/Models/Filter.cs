namespace TodoProApp
{

    public enum FilterType
    {
        ByStatus,
        ByToday, // 合并成 ByDueDate
        ByWeek,  //
        ByLabel
    }

    public static class TodoStatusToTitle
    {
        public static string ToTitle(this TodoStatus status)
        {
            if (status == TodoStatus.Pending)
            {
                return "收件箱";
            } else if (status == TodoStatus.Completed)
            {
                return "已完成";
            }

            return "";
        }
        
    }

    /// <summary>
    /// 过滤对象
    /// </summary>
    public class Filter
    {
        public static Filter ByStatus(TodoStatus status)
        {
            return new Filter()
            {
                Title = status.ToTitle(),
                TodoStatus = status,
                FilterType = FilterType.ByStatus
            };
        }

        public static Filter ByToday()
        {
            return new Filter()
            {
                Title = "今天",
                TodoStatus = TodoStatus.Pending,
                FilterType = FilterType.ByToday
            };
        }
        
        public static Filter ByWeek()
        {
            return new Filter()
            {
                Title = "一周内",
                TodoStatus = TodoStatus.Pending,
                FilterType = FilterType.ByWeek
            };
        }

        public static Filter ByLabel(Label label)
        {
            return new Filter()
            {
                Title = label.Name,
                TodoStatus = TodoStatus.Pending,
                FilterType = FilterType.ByLabel,
                Label = label
            };
        }

        public string Title;
        
        public TodoStatus TodoStatus = TodoStatus.Pending;

        public Label Label;

        public FilterType FilterType;
    }
}
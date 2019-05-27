namespace TodoProApp
{

    public enum FilterType
    {
        Inbox,
        Finished
    }

    /// <summary>
    /// 过滤对象
    /// </summary>
    public class Filter
    {
        public static Filter ByInbox()
        {
            return new Filter()
            {
                Title = "收件箱",
                TodoStatus = TodoStatus.Pending,
                FilterType = FilterType.Inbox
            };
        }

        public static Filter ByFinished()
        {
            return new Filter()
            {
                Title = "已完成",
                TodoStatus = TodoStatus.Completed,
                FilterType = FilterType.Finished
            };
        }

        public string Title;
        
        public TodoStatus TodoStatus = TodoStatus.Pending;

        public FilterType FilterType;
    }
}
namespace TodoProApp
{
    public enum FilterType
    {
        ByStatus,
        ByDueDate,
        ByLabel,
        ByProject
    }

    public static class TodoStatusToTitle
    {
        public static string ToTitle(this TodoStatus status)
        {
            if (status == TodoStatus.Pending)
            {
                return "收件箱";
            }
            else if (status == TodoStatus.Completed)
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


        public static Filter ByDueDate(DueDate dueDate)
        {
            return new Filter()
            {
                Title = dueDate.ToTitle(),
                TodoStatus = TodoStatus.Pending,
                DueDate = dueDate,
                FilterType = FilterType.ByDueDate
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

        public static Filter ByProject(Project project)
        {
            return new Filter()
            {
                Title = project.Name,
                TodoStatus = TodoStatus.Pending,
                FilterType = FilterType.ByProject,
                Project = project
            };
        }

        public string Title;

        public TodoStatus TodoStatus = TodoStatus.Pending;

        public Label Label;

        public Project Project;

        public FilterType FilterType;

        public DueDate DueDate;
    }
}
using Unity.UIWidgets.material;
using Unity.UIWidgets.ui;

namespace TodoProApp
{
    public enum Priority
    {
        Priority1 = 0,
        Priority2 = 1,
        Priority3 = 2,
        Priority4 = 3,
    }
    

    public static class PriorityExtension
    {
        public static Color ToColor(this Priority priority)
        {
            if (priority == Priority.Priority1)
            {
                return Colors.red;
            }
            else if (priority == Priority.Priority2)
            {
                return Colors.orange;
            }
            else if (priority == Priority.Priority3)
            {
                return Colors.yellow;
            }
            else if (priority == Priority.Priority4)
            {
                return Colors.white;
            }

            return Colors.white; 
        }
        
        public static string ToText(this Priority priority)
        {
            if (priority == Priority.Priority1)
            {
                return "优先级 1";
            }
            else if (priority == Priority.Priority2)
            {
                return "优先级 2";
            }
            else if (priority == Priority.Priority3)
            {
                return "优先级 3";
            }
            else if (priority == Priority.Priority4)
            {
                return "优先级 4";
            }

            return "没有优先级";
        }
    }
}
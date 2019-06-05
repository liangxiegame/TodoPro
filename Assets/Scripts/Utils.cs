using System.Collections.Generic;
using System.Text;
using Unity.UIWidgets.material;

namespace TodoProApp
{

    public class ColorObject
    {
        public static List<ColorObject> Presets = new List<ColorObject>()
        {
            new ColorObject(){Name = "红色",Value = Colors.red.value},
            new ColorObject(){Name = "粉色",Value = Colors.pink.value},
            new ColorObject(){Name = "紫色",Value = Colors.purple.value},
            new ColorObject(){Name = "暗紫色",Value = Colors.deepPurple.value},
            new ColorObject(){Name = "靛蓝",Value = Colors.indigo.value},
            new ColorObject(){Name = "蓝色",Value = Colors.blue.value},
            new ColorObject(){Name = "亮蓝色",Value = Colors.lightBlue.value},
            new ColorObject(){Name = "蓝绿色",Value = Colors.cyan.value},
            new ColorObject(){Name = "青色",Value = Colors.teal.value},
            new ColorObject(){Name = "绿色",Value = Colors.green.value},
            new ColorObject(){Name = "亮绿色",Value = Colors.lightGreen.value},
            new ColorObject(){Name = "绿黄色",Value = Colors.lime.value},
            new ColorObject(){Name = "黄色",Value = Colors.yellow.value},
            new ColorObject(){Name = "琥珀色",Value = Colors.amber.value},
            new ColorObject(){Name = "橙色",Value = Colors.orange.value},
            new ColorObject(){Name = "暗橙色",Value = Colors.deepOrange.value},
            new ColorObject(){Name = "棕色",Value = Colors.brown.value},
            new ColorObject(){Name = "黑色",Value = Colors.black.value},
            new ColorObject(){Name = "灰色",Value = Colors.grey.value},
        };
        
        public string Name;

        public long Value;
    }
    
    public static class Utils
    {
        public static string GetLabelString(List<string> labelIds, List<Label> labels)
        {
            var stringBuilder = new StringBuilder();

            labelIds.ForEach(id =>
            {
                var labelName = labels.Find(label=>label.Id == id).Name;
                stringBuilder.Append(labelName).Append(" ");
            });

            return stringBuilder.ToString();
        }

        public static string GetProjectName(string projectId,List<Project> projects)
        {
            var project = projects.Find(proj => proj.Id == projectId);

            if (project != null)
            {
                return project.Name;
            }

            return "收件箱";
        }
    }
}
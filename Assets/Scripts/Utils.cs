using System.Collections.Generic;
using System.Text;

namespace TodoProApp
{
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
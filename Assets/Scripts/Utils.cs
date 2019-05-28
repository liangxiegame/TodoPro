using System.Collections.Generic;
using System.Text;

namespace TodoProApp
{
    public class Utils
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
    }
}
using System;
using Unity.UIWidgets.ui;

namespace TodoProApp
{
    public class Project
    {   
        public string Id { get; set; } = Guid.NewGuid().ToString();
        
        public string Name { get; set; }

        public int ColorIndex { get; set; } = 0;
    }

    public static class ProjectExtension
    {
        public static Color GetColor(this Project project)
        {
            return new Color(ColorObject.Presets[project.ColorIndex].Value);
        }
        
        public static string GetName(this Project project)
        {
            return ColorObject.Presets[project.ColorIndex].Name;
        }
    }
}
using System;
using Unity.UIWidgets.ui;

namespace TodoProApp
{
    public class Label
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        
        public string Name { get; set; }

        public int ColorIndex { get; set; } = 0;
    }


    public static class LabelExtension
    {
        public static Color GetColor(this Label label)
        {
            return new Color(ColorObject.Presets[label.ColorIndex].Value);
        }
    }
}
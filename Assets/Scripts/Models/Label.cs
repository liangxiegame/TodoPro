using System;

namespace TodoProApp
{
    public class Label
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        
        public string Name { get; set; }
        
        // color
    }
}
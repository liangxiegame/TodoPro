using System;

namespace TodoProApp
{
    public class Todo
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public string Title;
        
    }
}
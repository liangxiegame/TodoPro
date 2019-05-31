using System;

namespace TodoProApp
{
    public class Project
    {   
        public string Id { get; set; } = Guid.NewGuid().ToString();
        
        public string Name { get; set; }
    }
}
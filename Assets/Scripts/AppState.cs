using System.Collections.Generic;
using QFramework.UIWidgets.ReduxPersist;
using UnityEditor.Animations;

namespace TodoProApp
{
    public class AppState : AbstractPersistState<AppState>
    {
        public List<Todo> Todos = new List<Todo>();
        
        public List<Label> Labels = new List<Label>();
        
        public Filter Filter = Filter.ByStatus(TodoStatus.Pending);
    }
}
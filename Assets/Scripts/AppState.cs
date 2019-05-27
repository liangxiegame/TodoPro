using System.Collections.Generic;
using QFramework.UIWidgets.ReduxPersist;

namespace TodoProApp
{
    public class AppState : AbstractPersistState<AppState>
    {
        public List<Todo> Todos = new List<Todo>();
        
        public Filter Filter = Filter.ByInbox();
    }
}
namespace TodoProApp
{
    public class AppReducer
    {
        public static AppState Reduce(AppState state, object action)
        {
            switch (action)
            {
                case AddTodoAction addTodoAction:
                    state.Todos.Add(addTodoAction.Todo);
                    return state;
            }
            
            return state;
        }
    }
}
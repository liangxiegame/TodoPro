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
                case RemoveTodoAction removeTodoAction:
                    state.Todos.Remove(removeTodoAction.Todo);
                    return state;
            }
            
            return state;
        }
    }
}
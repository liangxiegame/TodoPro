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
                case CompleteTodoAction completeTodoAction:
                    completeTodoAction.Todo.Status = TodoStatus.Completed;
                    return state;
                case RemoveTodoAction removeTodoAction:
                    state.Todos.Remove(removeTodoAction.Todo);
                    return state;
                case ApplyFilterAction applyFilterAction:
                    state.Filter = applyFilterAction.Filter;
                    return state;
                case UpdateTodoAction _:
                    return state;
                
                case AddLabelAction addLabelAction:
                    state.Labels.Add(addLabelAction.Label);
                    return state;
                
                case AddProjectAction addProjectAction:
                    state.Projects.Add(addProjectAction.Project);
                    return state;
            }

            return state;
        }
    }
}
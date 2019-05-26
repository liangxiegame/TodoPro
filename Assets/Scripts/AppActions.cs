namespace TodoProApp
{
    public class AddTodoAction
    {
        public AddTodoAction(Todo todo)
        {
            Todo = todo;
        }

        public Todo Todo { get; }
        
    }
}
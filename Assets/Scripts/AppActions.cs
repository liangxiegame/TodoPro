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

    public class CompleteTodoAction
    {
        public CompleteTodoAction(Todo todo)
        {
            Todo = todo;
        }

        public Todo Todo { get; }

    }

    public class RemoveTodoAction
    {
        public Todo Todo { get; }

        public RemoveTodoAction(Todo todo)
        {
            this.Todo = todo;
        }
    }
}
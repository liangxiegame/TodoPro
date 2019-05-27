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

    public class RemoveTodoAction
    {
        public Todo Todo { get; }

        public RemoveTodoAction(Todo todo)
        {
            this.Todo = todo;
        }
    }
}
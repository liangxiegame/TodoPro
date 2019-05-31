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

    
    public class UpdateTodoAction
    {
        public UpdateTodoAction(Todo todo)
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


    public class ApplyFilterAction
    {
        public ApplyFilterAction(Filter filter)
        {
            Filter = filter;
        }

        public Filter Filter { get; }
    }

    public class AddLabelAction
    {
        public AddLabelAction(Label label)
        {
            Label = label;
        }

        public Label Label { get; }
    }

    public class AddProjectAction
    {
        public Project Project { get; }
        
        public AddProjectAction(Project project)
        {
            Project = project;
        }
    }
    
}
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Unity.UIWidgets.material;
using Unity.UIWidgets.Redux;
using Unity.UIWidgets.widgets;

namespace TodoProApp
{
    public class Todos : StatelessWidget
    {
        public override Widget build(BuildContext context)
        {
            return new StoreConnector<AppState, TodosViewModel>(
                converter: state => new TodosViewModel(state.Filter, state.Todos,state.Labels),
                builder: (buildContext, model, dispatcher) =>
                {
                    return ListView.builder(
                        itemCount: model.Todos.Count,
                        itemBuilder: (context1, index) =>
                        {
                            var todo = model.Todos[index];
                            return new TodoWidget(todo,dispatcher,model.Labels);
                        });
                }
            );
        }
    }


    class TodosViewModel
    {
        public List<Todo> Todos { get; }
        
        public List<Label> Labels { get; }


        public TodosViewModel(Filter filter, List<Todo> todos, List<Label> labels)
        {
            Labels = labels;
            if (filter.FilterType == FilterType.ByStatus)
            {
                var availableTodos = todos
                    .Where(todo => todo.Status == filter.TodoStatus);

                if (filter.TodoStatus == TodoStatus.Pending)
                {
                    availableTodos = availableTodos.Where(todo => todo.DueDate == DueDate.None);
                }

                Todos = availableTodos.ToList();
            }
            else if (filter.FilterType == FilterType.ByToday)
            {
                Todos = todos
                    .Where(todo => todo.DueDate == DueDate.Today &&
                                   todo.Status == TodoStatus.Pending)
                    .ToList();
            } else if (filter.FilterType == FilterType.ByWeek)
            {
                Todos = todos
                    .Where(todo => todo.DueDate == DueDate.Next7Day &&
                                   todo.Status == TodoStatus.Pending)
                    .ToList();
            } else if (filter.FilterType == FilterType.ByLabel)
            {
                Todos = todos
                    .Where(todo => todo.Status == TodoStatus.Pending && todo.Labels.Contains(filter.Label.Id))
                    .ToList();
            }
        }
    }
}
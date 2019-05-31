using System.Collections.Generic;
using System.IO;
using System.Linq;
using Unity.UIWidgets.material;
using Unity.UIWidgets.Redux;
using Unity.UIWidgets.widgets;
using UnityEditor.Experimental.UIElements.GraphView;
using UnityEngine;

namespace TodoProApp
{
    public class Todos : StatelessWidget
    {
        public override Widget build(BuildContext context)
        {
            return new StoreConnector<AppState, TodosViewModel>(
                converter: state => new TodosViewModel(state.Filter, state.Todos, state.Labels,state.Projects),
                builder: (buildContext, model, dispatcher) =>
                {
                    return ListView.builder(
                        itemCount: model.Todos.Count,
                        itemBuilder: (context1, index) =>
                        {
                            var todo = model.Todos[index];
                            return new TodoWidget(todo, dispatcher, model.Labels,model.Projects);
                        });
                }
            );
        }
    }


    class TodosViewModel
    {
        public List<Todo> Todos { get; }

        public List<Label> Labels { get; }
        public List<Project> Projects { get; }


        public TodosViewModel(Filter filter, List<Todo> todos, List<Label> labels, List<Project> projects)
        {
            Labels = labels;
            Projects = projects;
            if (filter.FilterType == FilterType.ByStatus)
            {
                var availableTodos = todos
                    .Where(todo => todo.Status == filter.TodoStatus);

                if (filter.TodoStatus == TodoStatus.Pending)
                {
                    availableTodos = availableTodos.Where(todo => 
                        todo.DueDate == DueDate.None &&
                        todo.Priority == Priority.Priority4 &&
                        todo.Labels.Count == 0);
                }

                Todos = availableTodos.ToList();
            }
            else if (filter.FilterType == FilterType.ByDueDate)
            {
                Todos = todos
                    .Where(todo => todo.DueDate == filter.DueDate &&
                                   todo.Status == TodoStatus.Pending)
                    .ToList();
            }
            else if (filter.FilterType == FilterType.ByLabel)
            {
                Todos = todos
                    .Where(todo => todo.Status == TodoStatus.Pending && todo.Labels.Contains(filter.Label.Id))
                    .ToList();
            }
            else if (filter.FilterType == FilterType.ByProject)
            {
                Todos = todos
                    .Where(todo => todo.Status == TodoStatus.Pending && todo.ProjectId == filter.Project.Id)
                    .ToList();
            }
            else
            {
                Todos = new List<Todo>();
            }
        }
    }
}
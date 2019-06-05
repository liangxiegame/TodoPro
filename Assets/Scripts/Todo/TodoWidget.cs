using System.Collections.Generic;
using UIWidgetsGallery.gallery;
using Unity.UIWidgets;
using Unity.UIWidgets.material;
using Unity.UIWidgets.painting;
using Unity.UIWidgets.rendering;
using Unity.UIWidgets.ui;
using Unity.UIWidgets.widgets;

namespace TodoProApp
{
    public class TodoWidget : StatelessWidget
    {
        public Todo       Todo       { get; }
        public Dispatcher Dispatcher { get; }

        public List<Label> Labels { get; }

        public List<Project> Projects { get; }

        public TodoWidget(Todo todo, Dispatcher dispatcher, List<Label> labels, List<Project> projects)
        {
            this.Todo = todo;
            this.Dispatcher = dispatcher;
            Labels = labels;
            Projects = projects;
        }

        public override Widget build(BuildContext context)
        {
            return new Dismissible(
                direction: Todo.DueDate == DueDate.Today ? DismissDirection.horizontal : DismissDirection.startToEnd,
                key: new ObjectKey(Todo),
                child: new GestureDetector(
                    child: new Column(
                        children: new List<Widget>()
                        {
                            new Container(
                                margin: EdgeInsets.symmetric(vertical: 2),
                                decoration: new BoxDecoration(
                                    border: new Border(
                                        left: new BorderSide(Todo.Priority.ToColor(),
                                            width: 4f)
                                    )
                                ),
                                child: new Padding(
                                    padding: EdgeInsets.all(8),
                                    child: new Column(
                                        crossAxisAlignment: CrossAxisAlignment.start,
                                        children: new List<Widget>()
                                        {
                                            new Padding(
                                                padding: EdgeInsets.only(left: 8, bottom: 4),
                                                child: new Text(Todo.Title,
                                                    style: new TextStyle(
                                                        fontSize: 16,
                                                        fontWeight: FontWeight.bold
                                                    )
                                                )
                                            ),
                                            Todo.Labels.Count == 0
                                                ? new Container() as Widget
                                                : new Padding(
                                                    padding: EdgeInsets.only(left: 8, bottom: 4),
                                                    child: new Text(Utils.GetLabelString(Todo.Labels, Labels),
                                                        style: new TextStyle(
                                                            fontSize: 14
                                                        )
                                                    )
                                                ),
                                            new Padding(
                                                padding: EdgeInsets.only(left: 8, bottom: 4),
                                                child: new Row(
                                                    children: new List<Widget>()
                                                    {
                                                        new Text(Todo.DueDate.ToText(),
                                                            style: new TextStyle(
                                                                color: Colors.grey,
                                                                fontSize: 12
                                                            )
                                                        ),
                                                        new Expanded(
                                                            child: new Column(
                                                                crossAxisAlignment: CrossAxisAlignment.end,
                                                                children: new List<Widget>()
                                                                {
                                                                    new Row(
                                                                        mainAxisAlignment: MainAxisAlignment.end,
                                                                        children: new List<Widget>()
                                                                        {
                                                                            new Text(Utils.GetProjectName(
                                                                                    Todo.ProjectId, Projects),
                                                                                style: new TextStyle(
                                                                                    fontSize: 14,
                                                                                    color: Colors.grey
                                                                                )
                                                                            ),
                                                                            new Container(
                                                                                margin: EdgeInsets.symmetric(
                                                                                    horizontal: 8),
                                                                                width: 8,
                                                                                height: 8,
                                                                                child: new CircleAvatar(
                                                                                    backgroundColor: Todo
                                                                                        .GetProject(Projects).GetColor()
                                                                                )
                                                                            )
                                                                        }
                                                                    )
                                                                }
                                                            )
                                                        )
                                                    }
                                                )
                                            )
                                        }
                                    )
                                )
                            )
                        }
                    ),
                    onTap: () =>
                    {
                        Navigator.of(context).push(new MaterialPageRoute(buildContext => new TodoEditor(Todo)));
                    }),
                background: new Container(
                    color: Colors.red,
                    child: new ListTile(
                        leading: new Icon(
                            icon: Icons.delete,
                            color: Colors.white
                        )
                    )
                ),
                secondaryBackground: new Container(
                    color: Colors.green,
                    child: new ListTile(
                        trailing: new Icon(
                            icon: Icons.check,
                            color: Colors.white
                        )
                    )
                ),
                onDismissed: direction =>
                {
                    var message = string.Empty;

                    if (direction == DismissDirection.startToEnd)
                    {
                        Dispatcher.dispatch(new RemoveTodoAction(Todo));
                        message = $"任务 {Todo.Title} 已删除";
                    }
                    else if (direction == DismissDirection.endToStart)
                    {
                        Dispatcher.dispatch(new CompleteTodoAction(Todo));
                        message = $"任务 {Todo.Title} 已完成";
                    }

                    var snackBar = new SnackBar(content: new Text(message));

                    Scaffold.of(context).showSnackBar(snackBar);
                });
        }
    }
}
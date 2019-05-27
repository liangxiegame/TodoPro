using System.Collections.Generic;
using System.Linq;
using Unity.UIWidgets.material;
using Unity.UIWidgets.Redux;
using Unity.UIWidgets.widgets;
using UnityEngine;

namespace TodoProApp
{
    public class Home : StatelessWidget
    {
        public override Widget build(BuildContext context)
        {
            return new Scaffold(
                appBar: new AppBar(
                    title: new Text("Inbox")
                ),
                body: new StoreConnector<AppState, List<Todo>>(
                    converter: state => state.Todos
                        .Where(todo=>todo.Status == TodoStatus.Pending)
                        .ToList(),
                    builder: (buildContext, model, dispatcher) =>
                    {
                        return ListView.builder(
                            itemCount: model.Count,
                            itemBuilder: (context1, index) =>
                            {
                                var todo = model[index];
                                return new Dismissible(
                                    key:new ObjectKey(todo),
                                    child: new ListTile(title: new Text(todo.Title)),
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
                                        if (direction == DismissDirection.startToEnd)
                                        {
                                            dispatcher.dispatch(new RemoveTodoAction(todo));
                                        }
                                        else if (direction == DismissDirection.endToStart)
                                        {
                                            dispatcher.dispatch(new CompleteTodoAction(todo));
                                        }
                                    }
                                );

                            }

                        );
                    }
                ),
                floatingActionButton: new FloatingActionButton(
                    child: new Icon(
                        icon: Icons.add
                    ),
                    onPressed: () =>
                    {
                        Navigator.of(context).push(new MaterialPageRoute(
                            builder: buildContext => new AddTodoPage()
                        ));
                    }
                )
            );
        }
    }
}
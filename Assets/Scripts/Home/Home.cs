using System.Collections.Generic;
using Unity.UIWidgets.material;
using Unity.UIWidgets.Redux;
using Unity.UIWidgets.widgets;

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
                body:new StoreConnector<AppState,List<Todo>>(
                    converter:state => state.Todos,
                    builder:(buildContext, model, dispatcher) =>
                    {
                        return ListView.builder(
                            itemCount:model.Count,
                            itemBuilder:((context1, index) =>
                            {
                                var todo = model[index];
                                return new ListTile(title: new Text(todo.Title));
                            })
                            
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
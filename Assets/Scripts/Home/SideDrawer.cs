using System.Collections.Generic;
using Unity.UIWidgets.material;
using Unity.UIWidgets.Redux;
using Unity.UIWidgets.widgets;
using UnityEngine;

namespace TodoProApp
{
    public class SideDrawer : StatelessWidget
    {
        public override Widget build(BuildContext context)
        {
            return new StoreConnector<AppState, object>(
                converter: state => null,
                builder: (buildContext, model, dispatcher) =>
                {
                    return new Drawer(
                        child: new ListView(
                            children: new List<Widget>()
                            {
                                new ListTile(
                                    leading: new Icon(Icons.inbox),
                                    title: new Text("收件箱"),
                                    onTap: () =>
                                    {
                                        dispatcher.dispatch(new ApplyFilterAction(Filter.ByStatus(TodoStatus.Pending)));
                                        Navigator.pop(context);
                                    }
                                ),
                                new ListTile(
                                    leading: new Icon(Icons.today),
                                    title: new Text("今天"),
                                    onTap: () =>
                                    {
                                        dispatcher.dispatch(new ApplyFilterAction(Filter.ByToday()));
                                        Navigator.pop(context);
                                    }
                                ),
                                new ListTile(
                                    leading: new Icon(Icons.today),
                                    title: new Text("一周内"),
                                    onTap: () =>
                                    {
                                        dispatcher.dispatch(new ApplyFilterAction(Filter.ByWeek()));
                                        Navigator.pop(context);
                                    }
                                ),
                                new LabelDrawerItem(),
                                new ListTile(
                                    leading: new Icon(Icons.done),
                                    title: new Text("已完成"),
                                    onTap: () =>
                                    {
                                        dispatcher.dispatch(
                                            new ApplyFilterAction(Filter.ByStatus(TodoStatus.Completed)));
                                        Navigator.pop(context);
                                    }
                                )
                            }
                        )
                    );
                });
        }
    }
}
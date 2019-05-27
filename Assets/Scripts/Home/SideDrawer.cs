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
                                        dispatcher.dispatch(new ApplyFilterAction(Filter.ByInbox()));
                                        Debug.Log("收件箱");
                                    }
                                ),
                                new ListTile(
                                    leading: new Icon(Icons.done),
                                    title: new Text("已完成"),
                                    onTap: () =>
                                    {
                                        Debug.Log("已完成");
                                        dispatcher.dispatch(new ApplyFilterAction(Filter.ByFinished()));
                                    }
                                )
                            }
                        )
                    );
                });
        }
    }
}
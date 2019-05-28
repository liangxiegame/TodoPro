using System.Collections.Generic;
using System.Linq;
using Unity.UIWidgets.material;
using Unity.UIWidgets.painting;
using Unity.UIWidgets.Redux;
using Unity.UIWidgets.widgets;
using UnityEngine;

namespace TodoProApp
{
    public class LabelDrawerItem : StatelessWidget
    {
        public override Widget build(BuildContext context)
        {
            return new StoreConnector<AppState, List<Label>>(
                converter: state => state.Labels,
                builder: (buildContext, model, dispatcher) =>
                {
                    return new ExpansionTile(
                        leading: new Icon(Icons.label),
                        title: new Text($"标签 {model.Count}"),
                        children: model.Select(label => new LabelRow(label) as Widget).ToList(),

                        trailing: new IconButton(
                            icon: new Icon(Icons.add),
                            onPressed: () =>
                            {
                                Navigator.of(context)
                                    .push(new MaterialPageRoute(buildContext1 => new LabelEditor()));
                            }));
//                        onTap: () =>
//                        {
//                            Debug.Log("tag list");
                    dispatcher.dispatch(new ApplyFilterAction(Filter.ByWeek()));
//                        });
                });
        }
    }

    class LabelRow : StatelessWidget
    {
        public LabelRow(Label label)
        {
            mLabel = label;
        }

        private Label mLabel { get; }

        public override Widget build(BuildContext context)
        {
            return new ListTile(
                leading: new Container(
                    width: 24,
                    height: 24
                ),
                title: new Text(mLabel.Name),
                trailing: new Container(
                    width: 10,
                    height: 10,
                    child: new Icon(
                        icon: Icons.label,
                        size: 16,
                        color: Colors.black
                    )
                ),
                onTap: () => { Navigator.of(context).pop(); }
            );
        }
    }
}
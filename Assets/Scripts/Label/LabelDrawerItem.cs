using System;
using System.Collections.Generic;
using System.Linq;
using Unity.UIWidgets;
using Unity.UIWidgets.material;
using Unity.UIWidgets.painting;
using Unity.UIWidgets.Redux;
using Unity.UIWidgets.widgets;
using UnityEngine;

namespace TodoProApp
{
    public class LabelDrawerItem : StatelessWidget
    {
        List<Widget> BuildLabels(List<Label> model, Dispatcher dispatcher,BuildContext context)
        {
            var retList = model
                .Select(label => new LabelRow(label,
                    () => { dispatcher.dispatch(new ApplyFilterAction(Filter.ByLabel(label))); }) as Widget)
                .ToList();

            retList.Add(new ListTile(
                leading: new Icon(Icons.add),
                title: new Text("创建标签"),
                onTap: () =>
                {
                    Navigator.of(context)
                        .push(new MaterialPageRoute(buildContext1 => new LabelEditor()));
                }
            ));

            return retList;
        }

        public override Widget build(BuildContext context)
        {
            return new StoreConnector<AppState, List<Label>>(
                converter: state => state.Labels,
                builder: (buildContext, model, dispatcher) =>
                {
                    return new ExpansionTile(
                        leading: new Icon(Icons.label),
                        title: new Text("标签"),
                        children: BuildLabels(model, dispatcher,context));
                });
        }
    }

    class LabelRow : StatelessWidget
    {
        private Action mOnClick;

        public LabelRow(Label label, Action onClick)
        {
            mLabel = label;
            mOnClick = onClick;
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
                        color: mLabel.GetColor()
                    )
                ),
                onTap: () =>
                {
                    mOnClick();
                    Navigator.of(context).pop();
                }
            );
        }
    }
}
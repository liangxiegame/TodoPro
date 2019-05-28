using System.Collections.Generic;
using UIWidgets.Runtime.material;
using Unity.UIWidgets.material;
using Unity.UIWidgets.painting;
using Unity.UIWidgets.Redux;
using Unity.UIWidgets.widgets;
using UnityEngine;

namespace TodoProApp
{
    public class LabelEditor : StatelessWidget
    {
        GlobalKey<FormState> mFormState = GlobalKey<FormState>.key();

        Label mLabel = new Label();

        public override Widget build(BuildContext context)
        {
            return new Scaffold(
                appBar: new AppBar(
                    title: new Text("创建 Label")
                ),
                floatingActionButton: new StoreConnector<AppState, object>(
                    converter: state => null,
                    builder: (buildContext, model, dispatcher) =>
                    {
                        return new FloatingActionButton(
                            child: new Icon(
                                icon: Icons.send,
                                color: Colors.white),
                            onPressed: () =>
                            {
                                // 创建 Label 操作
                                if (mFormState.currentState.validate())
                                {
                                    mFormState.currentState.save();

                                    dispatcher.dispatch(new AddLabelAction(mLabel));

                                    Navigator.pop(context);
                                }
                            });
                    }
                ),
                body: new ListView(
                    children: new List<Widget>()
                    {
                        new Form(
                            key: mFormState,
                            child: new Padding(
                                padding: EdgeInsets.all(8),
                                child: new TextFormField(
                                    validator: value =>
                                    {
                                        if (!string.IsNullOrWhiteSpace(value))
                                        {
                                            return null;
                                        }

                                        return "Label name 不能为空";
                                    },
                                    onSaved: value => { mLabel.Name = value; },
                                    decoration: new InputDecoration(
                                        hintText: "Label name"
                                    )
                                )
                            )
                        ),
                        new Padding(
                            padding: EdgeInsets.all(4),
                            child: new Container(
                                height: 80,
                                color: Colors.black
                            )
                        )
                    }
                )
            );
        }
    }
}
using System.Collections.Generic;
using System.Linq;
using UIWidgets.Runtime.material;
using UIWidgetsGallery.gallery;
using Unity.UIWidgets.material;
using Unity.UIWidgets.painting;
using Unity.UIWidgets.Redux;
using Unity.UIWidgets.ui;
using Unity.UIWidgets.widgets;

namespace TodoProApp
{
    public class LabelEditor : StatefulWidget
    {
        public override State createState()
        {
            return new LabelEditorState();
        }
    }

    class LabelEditorState : State<LabelEditor> {
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
                            padding: EdgeInsets.only(top: 4),
                            child: new ExpansionTile(
                                leading: new Container(
                                    width: 12,
                                    height: 12,
                                    child: new CircleAvatar(
                                        backgroundColor: new Color(ColorObject.Presets[mLabel.ColorIndex].Value)
                                    )
                                ),
                                title: new Text(ColorObject.Presets[mLabel.ColorIndex].Name),
                                children: ColorObject.Presets.Select(
                                    colorObj => new ListTile(
                                        leading: new Container(
                                            width: 12,
                                            height: 12,
                                            child: new CircleAvatar(
                                                backgroundColor: new Color(colorObj.Value)
                                            )),
                                        title: new Text(colorObj.Name),
                                        onTap: () =>
                                        {
                                            this.setState(() =>
                                            {
                                                var colorIndex = ColorObject.Presets.IndexOf(colorObj);
                                                mLabel.ColorIndex = colorIndex;
                                            });
                                        }
                                    ) as Widget).ToList())
                        )
                    }
                )
            );
        }
    }
}
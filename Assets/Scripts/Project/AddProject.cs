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
    public class AddProject : StatefulWidget
    {
        public override State createState()
        {
            return new AddProjectState();
        }

        public Project Project { get; } = new Project();
    }

    class AddProjectState : State<AddProject>
    {
        GlobalKey<FormState> mFormState = GlobalKey<FormState>.key();

        public override Widget build(BuildContext context)
        {
            return new Scaffold(
                appBar: new AppBar(
                    title: new Text("创建项目")
                ),
                floatingActionButton: new StoreConnector<AppState, AppState>(
                    converter: state => state,
                    builder: (buildContext, model, dispatcher) =>
                    {
                        return new FloatingActionButton(
                            child: new Icon(Icons.send, color: Colors.white),
                            onPressed: () =>
                            {
                                if (mFormState.currentState.validate())
                                {
                                    mFormState.currentState.save();

                                    dispatcher.dispatch(new AddProjectAction(widget.Project));

                                    Navigator.pop(context);
                                }
                            }
                        );
                    }),
                body: new ListView(
                    children: new List<Widget>()
                    {
                        new Form(
                            key: mFormState,
                            child: new Padding(
                                padding: EdgeInsets.all(8),
                                child: new TextFormField(
                                    validator: (value) =>
                                    {
                                        if (string.IsNullOrWhiteSpace(value))
                                        {
                                            return "项目名不能为空";
                                        }

                                        return null;
                                    },
                                    onSaved: value => { widget.Project.Name = value; },
                                    initialValue: widget.Project.Name,
                                    decoration: new InputDecoration(
                                        hintText: "项目名"
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
                                        backgroundColor: new Color(ColorObject.Presets[widget.Project.ColorIndex].Value)
                                    )
                                ),
                                title: new Text(ColorObject.Presets[widget.Project.ColorIndex].Name),
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
                                                widget.Project.ColorIndex = colorIndex;
                                            });
                                        }
                                    ) as Widget).ToList())
                        )
                    })
            );
        }
    }
}
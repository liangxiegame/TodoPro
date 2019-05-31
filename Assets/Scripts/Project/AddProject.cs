using UIWidgets.Runtime.material;
using Unity.UIWidgets.material;
using Unity.UIWidgets.painting;
using Unity.UIWidgets.Redux;
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
                body:
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
                )
            );
        }
    }
}
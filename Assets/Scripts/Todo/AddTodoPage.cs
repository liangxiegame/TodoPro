using UIWidgets.Runtime.material;
using Unity.UIWidgets.material;
using Unity.UIWidgets.painting;
using Unity.UIWidgets.Redux;
using Unity.UIWidgets.widgets;

namespace TodoProApp
{
    public class AddTodoPage : StatelessWidget
    {
        GlobalKey<FormState> mFormState = GlobalKey<FormState>.key();
        
        Todo mTodo = new Todo();
        
        public override Widget build(BuildContext context)
        {
            return new Scaffold(
                appBar: new AppBar(
                    title: new Text("Add Todo")
                ),
                body: new Form(
                    key: mFormState,
                    child: new Padding(
                        padding: EdgeInsets.all(8),
                        child: new TextFormField(
                            onSaved: value => { mTodo.Title = value; },
                            validator: (value) =>
                            {
                                if (string.IsNullOrWhiteSpace(value))
                                {
                                    return "Title Cannot be Empty";
                                }

                                return null;
                            },
                            decoration: new InputDecoration(
                                hintText: "title"
                            )
                        )
                    )
                ),
                floatingActionButton: new StoreConnector<AppState, AppState>(
                    converter: state => state,
                    builder: (buildContext, model, dispatcher) =>
                    {
                        return new FloatingActionButton(
                            child: new Icon(Icons.send),
                            onPressed: () =>
                            {
                                if (mFormState.currentState.validate())
                                {
                                    mFormState.currentState.save();

                                    dispatcher.dispatch(new AddTodoAction(mTodo));

                                    Navigator.pop(context);
                                }

                            }
                        );
                    }));
        }
    }
}
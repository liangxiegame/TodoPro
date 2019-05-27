using System.Collections.Generic;
using System.Linq;
using UIWidgets.Runtime.material;
using Unity.UIWidgets.material;
using Unity.UIWidgets.painting;
using Unity.UIWidgets.Redux;
using Unity.UIWidgets.widgets;

namespace TodoProApp
{
    public enum EditorMode
    {
        Creation,
        Modification
    }
    
    public class TodoEditor : StatefulWidget
    {
        public Todo Todo { get; }
        
        public EditorMode Mode { get; }

        public TodoEditor(Todo todo = null)
        {
            if (todo != null)
            {
                Mode = EditorMode.Modification;
                Todo = todo;
            }
            else
            {
                Mode = EditorMode.Creation;
                Todo = new Todo();
            }
        }
        
        public override State createState()
        {
            return new TodoEditorState();
        }
    }

    class TodoEditorState : State<TodoEditor>
    {
        GlobalKey<FormState> mFormState = GlobalKey<FormState>.key();


        private string mTodoTitle = "";
        private DueDate mDueDate = DueDate.None;

        public override void initState()
        {
            base.initState();

            mTodoTitle = widget.Todo.Title;
            mDueDate = widget.Todo.DueDate;


        }

        public override Widget build(BuildContext context)
        {
            var title = widget.Mode == EditorMode.Creation ? "创建 Todo" : "编辑 Todo";
            
            return new Scaffold(
                appBar: new AppBar(
                    title: new Text(title)
                ),
                body: new ListView(
                    children: new List<Widget>()
                    {
                        new Form(
                            key: mFormState,
                            child: new Padding(
                                padding: EdgeInsets.all(8),
                                child: new TextFormField(
                                    initialValue:mTodoTitle,
                                    onSaved: value => { mTodoTitle = value; },
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

                        new ListTile(
                            leading: new Icon(Icons.calendar_today),
                            title: new Text("Due Date"),
                            trailing: new DropdownButton<string>(
                                value: mDueDate.ToString(),
                                onChanged: newValue =>
                                {
                                    setState(() =>
                                    {
                                        if (newValue == "None")
                                        {
                                            mDueDate = DueDate.None;
                                        } else if (newValue == "Today")
                                        {
                                            mDueDate = DueDate.Today;
                                        } else if (newValue == "Next 7 Day")
                                        {
                                            mDueDate = DueDate.Next7Day;
                                        }
                                    });
                                    
                                },
                                items: new List<string>
                                    {
                                        DueDate.None.ToText(),
                                        DueDate.Today.ToText(),
                                        DueDate.Next7Day.ToText()
                                    }
                                    .Select(value => new DropdownMenuItem<string>(
                                        value: value,
                                        child: new Text(value)
                                    )).ToList()
                            )
                        )
                    }
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

                                    widget.Todo.Title = mTodoTitle;
                                    widget.Todo.DueDate = mDueDate;
                                    
                                    if (widget.Mode == EditorMode.Creation)
                                    {
                                        dispatcher.dispatch(new AddTodoAction(widget.Todo));
                                    }
                                    else
                                    {
                                        dispatcher.dispatch(new UpdateTodoAction(widget.Todo));
                                    }

                                    Navigator.pop(context);
                                }
                            }
                        );
                    }));
        }
    }
}
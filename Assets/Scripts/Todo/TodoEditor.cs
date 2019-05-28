using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UIWidgets.Runtime.material;
using Unity.UIWidgets.material;
using Unity.UIWidgets.painting;
using Unity.UIWidgets.Redux;
using Unity.UIWidgets.widgets;
using UnityEditor.Animations;
using UnityEngine;
using DialogUtils = Unity.UIWidgets.material.DialogUtils;

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


        private string       mTodoTitle = "";
        private DueDate      mDueDate   = DueDate.None;
        private List<string> mLabels    = new List<string>();

        public override void initState()
        {
            base.initState();

            mTodoTitle = widget.Todo.Title;
            mDueDate = widget.Todo.DueDate;
            mLabels = widget.Todo.Labels;
        }

        public override Widget build(BuildContext context)
        {
            var title = widget.Mode == EditorMode.Creation ? "创建 Todo" : "编辑 Todo";

            return new StoreConnector<AppState, AppState>(
                converter: state => state,
                builder: (buildContext, model, dispatcher) =>
                {
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
                                            initialValue: mTodoTitle,
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
                                        value: mDueDate.ToText(),
                                        onChanged: newValue =>
                                        {
                                            setState(() =>
                                            {
                                                if (newValue == "None")
                                                {
                                                    mDueDate = DueDate.None;
                                                }
                                                else if (newValue == "Today")
                                                {
                                                    mDueDate = DueDate.Today;
                                                }
                                                else if (newValue == "Next 7 Day")
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
                                ),
                                new ListTile(
                                    leading: new Icon(icon: Icons.label),
                                    title: new Text("Labels"),
                                    subtitle: mLabels.Count == 0
                                        ? new Text("No Labels")
                                        : new Text(new Func<string>(() =>
                                        {
                                            return Utils.GetLabelString(mLabels, model.Labels);
                                        }).Invoke()),
                                    onTap: () => { ShowLabelsDialog(context); }
                                )
                            }),
                        floatingActionButton:
                        new FloatingActionButton(
                            child: new Icon(Icons.send),
                            onPressed: () =>
                            {
                                if (mFormState.currentState.validate())
                                {
                                    mFormState.currentState.save();

                                    widget.Todo.Title = mTodoTitle;
                                    widget.Todo.DueDate = mDueDate;
                                    widget.Todo.Labels = mLabels;

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
                        ));
                });
        }


        void ShowLabelsDialog(BuildContext context)
        {
            DialogUtils.showDialog(
                context: context,
                builder: buildContext =>
                {
                    return new StoreConnector<AppState, List<Label>>(
                        converter: state => state.Labels,
                        builder: (context1, model, dispatcher) =>
                        {
                            return new SimpleDialog(
                                title: new Text("Select Labels"),
                                children: model.Select(label =>
                                    {
                                        return new ListTile(
                                            leading: new Icon(icon: Icons.label, size: 18, color: Colors.black),
                                            title: new Text(label.Name),
                                            trailing: mLabels.Any(id => id == label.Id)
                                                ? new Icon(icon: Icons.close) as Widget
                                                : new Container(width: 18, height: 18),
                                            onTap: () =>
                                            {
                                                if (mLabels.Any(id => id == label.Id))
                                                {
                                                    mLabels.Remove(label.Id);
                                                }
                                                else
                                                {
                                                    mLabels.Add(label.Id);
                                                }

                                                Navigator.pop(context);
                                                
                                                this.setState(()=>{});

                                            }
                                        ) as Widget;
                                    })
                                    .ToList());
                        }
                    );
                });
        }
    }
}
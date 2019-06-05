using System;
using System.Collections.Generic;
using System.Linq;
using UIWidgets.Runtime.material;
using UIWidgetsGallery.gallery;
using Unity.UIWidgets;
using Unity.UIWidgets.material;
using Unity.UIWidgets.painting;
using Unity.UIWidgets.Redux;
using Unity.UIWidgets.widgets;
using Color = Unity.UIWidgets.ui.Color;
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
        private Priority     mPriority  = Priority.Priority4;
        private string       mProjectId = "1";

        public override void initState()
        {
            base.initState();

            mTodoTitle = widget.Todo.Title;
            mDueDate = widget.Todo.DueDate;
            mLabels = widget.Todo.Labels;
            mPriority = widget.Todo.Priority;
            mProjectId = widget.Todo.ProjectId;
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
                                    leading: new Icon(icon: Icons.book),
                                    title: new Text("项目"),
                                    subtitle: new Text(Utils.GetProjectName(mProjectId,model.Projects)),
                                    onTap: () => { ShowProjectDialog(context); }
                                ),
                                new ListTile(
                                    leading: new Icon(Icons.calendar_today),
                                    title: new Text("开始时间"),
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
                                    title: new Text("标签"),
                                    subtitle: mLabels.Count == 0
                                        ? new Text("没有标签")
                                        : new Text(new Func<string>(() =>
                                        {
                                            return Utils.GetLabelString(mLabels, model.Labels);
                                        }).Invoke()),
                                    onTap: () => { ShowLabelsDialog(context); }
                                ),
                                new ListTile(
                                    leading: new Icon(Icons.flag),
                                    title: new Text("优先级"),
                                    subtitle: new Text(mPriority.ToText()),
                                    onTap: () => { ShowPriorityDialog(context); }
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
                                    widget.Todo.Priority = mPriority;
                                    widget.Todo.ProjectId = mProjectId;

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

        void ShowProjectDialog(BuildContext context)
        {
            DialogUtils.showDialog(
                context: context,
                builder: buildContext =>
                {
                    return new StoreConnector<AppState, List<Project>>(
                        converter: state => state.Projects,
                        builder: (context1, model, dispatcher) =>
                        {
                            return new SimpleDialog(
                                title: new Text("选择项目"),
                                children: BuildProjectItem(model, dispatcher, context));
                        }
                    );
                });
        }

        List<Widget> BuildProjectItem(List<Project> projects, Dispatcher dispatcher, BuildContext context)
        {
            return projects.Select(project => new ListTile(
                leading: new Container(
                    width: 12,
                    height: 12,
                    child: new CircleAvatar(
                        backgroundColor: project.GetColor()
                    )),
                title: new Text(project.Name),
                onTap: () =>
                {
                    mProjectId = project.Id;

                    Navigator.pop(context);
                    setState(() => { });
                }
            ) as Widget).ToList();
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
                                title: new Text("选择标签"),
                                children: model.Select(label =>
                                    {
                                        return new ListTile(
                                            leading: new Icon(icon: Icons.label, size: 18, color: label.GetColor()),
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

                                                this.setState(() => { });
                                            }
                                        ) as Widget;
                                    })
                                    .ToList());
                        }
                    );
                });
        }

        void ShowPriorityDialog(BuildContext context)
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
                                title: new Text("选择优先级"),
                                children: new List<Widget>()
                                {
                                    PriorityItem(Priority.Priority1, context1),
                                    PriorityItem(Priority.Priority2, context1),
                                    PriorityItem(Priority.Priority3, context1),
                                    PriorityItem(Priority.Priority4, context1)
                                });
                        }
                    );
                });
        }

        Widget PriorityItem(Priority priority, BuildContext context)
        {
            return new GestureDetector(
                child: new Container(
                    color: mPriority == priority ? Colors.grey : Color.white,
                    child: new Container(
                        margin: EdgeInsets.symmetric(2),
                        decoration: new BoxDecoration(
                            border: new Border(
                                left: new BorderSide(
                                    width: 6,
                                    color: priority.ToColor()
                                )
                            )
                        ),
                        child: new Container(
                            margin: EdgeInsets.all(12),
                            child: new Text(priority.ToText(),
                                style: new TextStyle(fontSize: 18)
                            )
                        )
                    )
                ),
                onTap: () =>
                {
                    mPriority = priority;
                    Navigator.pop(context);
                    this.setState(() => { });
                }
            );
        }
    }
}
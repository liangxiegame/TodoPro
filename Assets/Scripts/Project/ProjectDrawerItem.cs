using System;
using System.Collections.Generic;
using System.Linq;
using UIWidgetsGallery.gallery;
using Unity.UIWidgets;
using Unity.UIWidgets.material;
using Unity.UIWidgets.Redux;
using Unity.UIWidgets.widgets;

namespace TodoProApp
{
    public class ProjectDrawerItem : StatelessWidget
    {
        List<Widget> BuildProjects(List<Project> model, Dispatcher dispatcher, BuildContext context)
        {
            var retList = model.Where(project => project.Id != "1")
                .Select(project => new ProjectRow(project,
                    () => { dispatcher.dispatch(new ApplyFilterAction(Filter.ByProject(project))); }) as Widget)
                .ToList();

            retList.Add(new ListTile(
                leading: new Icon(Icons.add),
                title: new Text("创建项目"),
                onTap: () =>
                {
                    Navigator.of(context)
                        .push(new MaterialPageRoute(buildContext1 => new AddProject()));
                }
            ));

            return retList;
        }

        public override Widget build(BuildContext context)
        {
            return new StoreConnector<AppState, List<Project>>(
                converter: state => state.Projects,
                builder: (buildContext, model, dispatcher) =>
                {
                    return new ExpansionTile(
                        leading: new Icon(icon: Icons.book),
                        title: new Text("项目"),
                        children: BuildProjects(model, dispatcher, context));
                });
        }
    }

    class ProjectRow : StatelessWidget
    {
        private Action mOnClick;

        public ProjectRow(Project project, Action onClick)
        {
            this.mProject = project;
            mOnClick = onClick;
        }

        private Project mProject { get; }

        public override Widget build(BuildContext context)
        {
            return new ListTile(
                leading: new Container(
                    width: 24,
                    height: 24
                ),
                title: new Text(mProject.Name),
                trailing: new Container(
                    width: 10,
                    height: 10,
                    child: new CircleAvatar(
                        backgroundColor: Colors.purple
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
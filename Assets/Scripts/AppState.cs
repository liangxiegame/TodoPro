using System.Collections.Generic;
using QFramework.UIWidgets.ReduxPersist;

namespace TodoProApp
{
    public class AppState : AbstractPersistState<AppState>
    {
        public List<Todo> Todos = new List<Todo>();

        public List<Label> Labels = new List<Label>();

        public List<Project> Projects = new List<Project>();

        public override void OnLoaded()
        {
            var inbox = Inbox;
        }

        public Project Inbox
        {
            get
            {
                var inboxProject = Projects.Find(project => project.Id == "1");

                if (inboxProject == null)
                {
                    inboxProject = new Project()
                    {
                        Id = "1",
                        Name = "收件箱"
                    };


                    Projects.Insert(0, inboxProject);
                }

                return inboxProject;
            }
        }

        public Filter Filter = Filter.ByStatus(TodoStatus.Pending);
    }
}
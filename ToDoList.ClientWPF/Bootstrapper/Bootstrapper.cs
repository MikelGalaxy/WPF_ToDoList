using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Practices.Unity;
using Prism.Events;
using Prism.Unity;
using ToDoList.ClientWPF.View;
using ToDoList.ClientWPF.ViewModel;

namespace ToDoList.ClientWPF
{
    public class Bootstrapper:UnityBootstrapper
    {
        protected override DependencyObject CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void ConfigureContainer()
        {
            base.ConfigureContainer();
            Container.RegisterType<IAddTaskViewModel, AddTaskViewModel>();
            Container.RegisterType<IMainWindowViewModel, MainWindowViewModel>();
            Container.RegisterType<ITaskListViewModel, TaskListViewModel>();
            Container.RegisterType<IEventAggregator, EventAggregator>();
            Container.RegisterType<TaskList, TaskList>(new ContainerControlledLifetimeManager());
            Container.RegisterType<AddTask, AddTask>(new ContainerControlledLifetimeManager());


        }

        protected override void InitializeShell()
        {
            base.InitializeShell();
            App.Current.MainWindow = (Window)Shell;
            App.Current.MainWindow.Show();
        }
    }
}

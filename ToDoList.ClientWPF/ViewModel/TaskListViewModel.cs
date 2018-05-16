using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.ClientWPF.Command;
using ToDoList.ClientWPF.Event;

namespace ToDoList.ClientWPF.ViewModel
{
    public class TaskListViewModel: BaseViewModel, ITaskListViewModel
    {
        private IEventAggregator _eventAggregator;

        public RelayCommand AddTaskCommand { get; set; }

        public TaskListViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            AddTaskCommand = new RelayCommand(OnAddTaskClick,CanAddTaskClick);
        }

        private bool CanAddTaskClick(object obj)
        {
            return true;
        }

        private void OnAddTaskClick(object obj)
        {
            _eventAggregator.GetEvent<CreateEditTaskEvent>().Publish(null);
        }


    }
}

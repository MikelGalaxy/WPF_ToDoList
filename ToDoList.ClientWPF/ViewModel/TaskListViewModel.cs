using Prism.Events;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ToDoList.ClientWPF.Command;
using ToDoList.ClientWPF.Event;

namespace ToDoList.ClientWPF.ViewModel
{
    public class TaskListViewModel: BaseViewModel, ITaskListViewModel
    {
        private IEventAggregator _eventAggregator;

        public RelayCommand AddTaskCommand { get; set; }

        public RelayCommand Exit { get; set; }

        public ObservableCollection<Task> TaskList { get; set; }

        public RelayCommand FilterAll { get; set; }
        public RelayCommand FilterOverdue { get; set; }
        public RelayCommand FilterToday { get; set; }
        public RelayCommand FilterThisWeek { get; set; }
        public RelayCommand IsFinished { get; set; }

        private bool _isFinished;

        public bool Finished
        {
            get { return _isFinished; }
            set { _isFinished = value;
                OnPropertyChanged();

            }
        }

        public bool isFinished { get; set; }

        public TaskListViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            AddTaskCommand = new RelayCommand(OnAddTaskClick,CanAddTaskClick);
            Exit = new RelayCommand(OnExitClick, CanExitClick);
            FilterAll = new RelayCommand(x => ClickFilterAll());
            FilterOverdue = new RelayCommand(x => ClickFilterOverdue());
            FilterToday = new RelayCommand(x => ClickFilterToday());
            FilterThisWeek = new RelayCommand(x => ClickFilterThisWeek());
            IsFinished = new RelayCommand(x => ClickIsFinished());
        }

        private void ClickIsFinished()
        {
            MessageBox.Show("xD Finished Toogled" + Finished);
        }

        private void ClickFilterThisWeek()
        {
            MessageBox.Show("xD This week");
        }

        private void ClickFilterToday()
        {
            MessageBox.Show("xD Today");
        }

        private void ClickFilterOverdue()
        {
            MessageBox.Show("xD Overdue");
        }

        private void ClickFilterAll()
        {
            //XD
            MessageBox.Show("xD All");
        }








        private bool CanExitClick(object obj)
        {
            return true;
        }

        private void OnExitClick(object obj)
        {
            System.Windows.Application.Current.Shutdown();
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

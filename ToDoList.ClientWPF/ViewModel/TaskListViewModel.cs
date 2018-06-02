using Prism.Events;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using ToDoList.ClientWPF.Command;
using ToDoList.ClientWPF.Event;
using ToDoList.Model;

namespace ToDoList.ClientWPF.ViewModel
{
    public class TaskListViewModel: BaseViewModel, ITaskListViewModel
    {
        private IEventAggregator _eventAggregator;

        public RelayCommand AddTaskCommand { get; set; }

        public RelayCommand Exit { get; set; }

        public ObservableCollection<ToDoTask> TaskList { get; set; }


        //public RelayCommand FilterAll { get; set; }
        public RelayCommand FilterOverdue { get; set; }
        //public RelayCommand FilterToday { get; set; }
        public RelayCommand FilterThisWeek { get; set; }
        //public RelayCommand IsFinished { get; set; }

        private bool _allFilter=true;

        public bool AllFilter
        {
            get { return _allFilter; }
            set { _allFilter = value;
                OnPropertyChanged();
                view.Refresh();
            }
        }



        private bool _todayFilter;
        public bool TodayFilter
        {
            get { return _todayFilter; }
            set { _todayFilter = value;
                OnPropertyChanged();
                view.Refresh();
            }
        }



        private bool _isFinished;
        public bool Finished
        {
            get { return _isFinished; }
            set { _isFinished = value;
                OnPropertyChanged();
                view.Refresh();
            }
        }

        public bool isFinished { get; set; }

        CollectionView view;


        public TaskListViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            TaskList = new ObservableCollection<ToDoTask>();
            AddTaskCommand = new RelayCommand(OnAddTaskClick,CanAddTaskClick);
            Exit = new RelayCommand(OnExitClick, CanExitClick);
            //FilterAll = new RelayCommand(x => ClickFilterAll());
            FilterOverdue = new RelayCommand(x => ClickFilterOverdue());
            //FilterToday = new RelayCommand(x => ClickFilterToday());
            FilterThisWeek = new RelayCommand(x => ClickFilterThisWeek());
            //IsFinished = new RelayCommand(x => ClickIsFinished());
            testSeed();
            view = (CollectionView)CollectionViewSource.GetDefaultView(TaskList);
            view.Filter += FinishedFilter;
            //view.Filter += TodayFilters;
            

        }

        private bool TodayFilters(object item)
        {
            ToDoTask taskToDo = (ToDoTask)item;
            
            if (TodayFilter == true)
            {
                if (taskToDo.DueDate == DateTime.Now.ToString("yyyy-MM-dd"))
                {
                    return true;
                }
                else
                    return false;
            }
            else
                return true;          
        }



        private bool FinishedFilter(object item)
        {
            ToDoTask taskToDo = (ToDoTask)item;
            DateTime date = DateTime.ParseExact(taskToDo.DueDate, "yyyy-MM-dd", new DateTimeFormatInfo());
            if (Finished == false)
            {
                if(TodayFilter)
                {
                    if (date.Date == DateTime.Now.Date)
                    {
                        return true;
                    }
                    else
                        return false;
                }


                return true;
            }
            else
            {
                if (TodayFilter)
                {
                    if (date.Date == DateTime.Now.Date && taskToDo.Completion == 100)
                    {
                        return true;
                    }
                    else
                        return false;
                }


                if (taskToDo.Completion == 100)
                {
                    return true;
                }
                else
                    return false;
            }
        }



        private void ClickFilterThisWeek()
        {
            MessageBox.Show("xD This week");
        }


        private void ClickFilterOverdue()
        {
            MessageBox.Show("xD Overdue");
        }






        private void testSeed()
        {
            TaskList.Add(new ToDoTask(false, "2018-04-06", "Some title", 30, "XD"));
            TaskList.Add(new ToDoTask(false, "2018-05-06", "Some title2", 40, "XD"));
            TaskList.Add(new ToDoTask(false, "2018-06-06", "Some title3", 100, "XD"));
            TaskList.Add(new ToDoTask(false, "2018-06-02", "555", 100, "XD"));
            TaskList.Add(new ToDoTask(false, "2018-06-02", "12345", 25, "XD"));
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

        public void SelectedTask(ToDoTask task)
        {
            if (task != null)
            {
                _eventAggregator.GetEvent<CreateEditTaskEvent>().Publish(task);
            }
        }
    }
}

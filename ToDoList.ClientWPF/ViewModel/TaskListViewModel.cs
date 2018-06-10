using Microsoft.Win32;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using ToDoList.ClientWPF.Command;
using ToDoList.ClientWPF.Event;
using ToDoList.IOService;
using ToDoList.Model;

namespace ToDoList.ClientWPF.ViewModel
{
    public class TaskListViewModel: BaseViewModel, ITaskListViewModel
    {
        private IEventAggregator _eventAggregator;

        public RelayCommand AddTaskCommand { get; set; }


        public RelayCommand LoadFile { get; set; }
        public RelayCommand Save { get; set; }
        public RelayCommand SaveAs { get; set; }
        public RelayCommand Exit { get; set; }



        public ObservableCollection<ToDoTask> TaskList { get; set; }


        private bool _allFilter=true;

        public bool AllFilter
        {
            get { return _allFilter; }
            set { _allFilter = value;
                OnPropertyChanged();
                view.Refresh();
            }
        }

        private bool _overdueFilter;

        public bool OverdueFilter
        {
            get { return _overdueFilter; }
            set { _overdueFilter = value;
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

        private bool _thisWeekFilter;

        public bool ThisWeekFilter
        {
            get { return _thisWeekFilter; }
            set { _thisWeekFilter = value;
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
        IOManager ioManager;
        CollectionView view;
        private ToDoTask editedTask;

        public TaskListViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            ioManager = new IOManager();
            TaskList = new ObservableCollection<ToDoTask>();
            AddTaskCommand = new RelayCommand(OnAddTaskClick,CanAddTaskClick);
            LoadFile = new RelayCommand(OnLoadFileClick, CanLoadFileClick);
            Save = new RelayCommand(OnSaveClick, CanSaveClick);
            //SaveAs = new RelayCommand(OnSaveAsClick, CanSaveAsClick);
            Exit = new RelayCommand(OnExitClick, CanExitClick);
            //testSeed();
            view = (CollectionView)CollectionViewSource.GetDefaultView(TaskList);
            view.Filter += Filters;
            eventAggregator.GetEvent<SendTaskToListEvent>().Subscribe(newTaskAdded);
            eventAggregator.GetEvent<SendEditedTaskToListEvent>().Subscribe(taskEdited);
        }

       

        private void taskEdited(ToDoTask item)
        {
            if(item!=null)
            {
                var t=TaskList.FirstOrDefault(x=>x.Equals(editedTask));
                if(t!=null)
                {
                    t.Checked = item.Checked;
                    t.Title = item.Title;
                    t.DueDate = item.DueDate;
                    t.Completion = item.Completion;
                    t.Description = item.Description;

                }                
                view.Refresh();
                //MessageBox.Show("Task edited");
            }
           
        }

        private void newTaskAdded(ToDoTask item)
        {
            if (item != null)
            {
                TaskList.Add(item);
                MessageBox.Show("Added task "+item.Title);
            }
               
        }

        private bool Filters(object item)
        {
            ToDoTask taskToDo = (ToDoTask)item;
            DateTime date = DateTime.ParseExact(taskToDo.DueDate, "yyyy-MM-dd", new DateTimeFormatInfo());
            if (Finished == false)
            {
                if (OverdueFilter)
                {
                    if (date.Date < DateTime.Now.Date)
                    {
                        return true;
                    }
                    else
                        return false;
                }
                if (TodayFilter)
                {
                    if (date.Date == DateTime.Now.Date)
                    {
                        return true;
                    }
                    else
                        return false;
                }
                if(ThisWeekFilter)
                {
                    CultureInfo cul = CultureInfo.CurrentCulture;
                    int itemWeek = cul.Calendar.GetWeekOfYear(date, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
                    int currentWeek = cul.Calendar.GetWeekOfYear(DateTime.Now, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
                    if (itemWeek == currentWeek)
                    {
                        return true;
                    }
                    else
                        return false;
                }
                //its for all filter
                return true;
            }
            else
            {
                if (OverdueFilter)
                {
                    if (date.Date < DateTime.Now.Date && taskToDo.Completion == 100)
                    {
                        return true;
                    }
                    else
                        return false;
                }
                if (TodayFilter)
                {
                    if (date.Date == DateTime.Now.Date && taskToDo.Completion == 100)
                    {
                        return true;
                    }
                    else
                        return false;
                }
                if (ThisWeekFilter)
                {
                    CultureInfo cul = CultureInfo.CurrentCulture;
                    int itemWeek = cul.Calendar.GetWeekOfYear(date, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
                    int currentWeek = cul.Calendar.GetWeekOfYear(DateTime.Now, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
                    if (itemWeek == currentWeek && taskToDo.Completion == 100)
                    {
                        return true;
                    }
                    else
                        return false;
                }
                //its for all filter
                if (taskToDo.Completion == 100)
                {
                    return true;
                }
                else
                    return false;
            }
        }


        private void testSeed()
        {
            TaskList.Add(new ToDoTask(false, "2018-04-06", "OVerdue fi", 100, "finished"));
            TaskList.Add(new ToDoTask(false, "2018-04-06", "Some title", 30, "XD"));
            TaskList.Add(new ToDoTask(false, "2018-05-06", "Some title2", 40, "XD"));
            TaskList.Add(new ToDoTask(false, "2018-06-06", "Some title3", 100, "XD"));
            TaskList.Add(new ToDoTask(false, "2018-06-02", "555", 100, "XD"));
            TaskList.Add(new ToDoTask(false, "2018-06-03", "555", 100, "XD"));
            TaskList.Add(new ToDoTask(false, "2018-05-29", "1111111", 25, "XD"));
            TaskList.Add(new ToDoTask(false, "2018-06-02", "12345", 25, "XD"));
            
        }

        #region Commands

        //private bool CanSaveAsClick(object obj)
        //{
        //    if (TaskList.Count > 0)
        //    {
        //        return true;
        //    }
        //    else
        //        return false;
        //}

        //private void OnSaveAsClick(object obj)
        //{
        //    MessageBox.Show("Saving to file!");
        //}

        private bool CanSaveClick(object obj)
        {
            if (TaskList.Count > 0)
            {
                return true;
            }
            else
                return false;
        }

        private void OnSaveClick(object obj)
        {
            var fileDialog = new SaveFileDialog();
            fileDialog.Filter = "TEXT |*.txt";
            fileDialog.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory;
            if (fileDialog.ShowDialog().Value)
            {                
                //MessageBox.Show("Saving file! "+fileDialog.FileName);
                ioManager.SaveFile(TaskList.ToList(),fileDialog.FileName);
            }
            
        }

        private bool CanLoadFileClick(object obj)
        {
            return true;
        }

        private void OnLoadFileClick(object obj)
        {
            var fileDialog = new OpenFileDialog();
            fileDialog.Filter = "TEXT |*.txt";
            fileDialog.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory;
            if (fileDialog.ShowDialog().Value)
            {
                string filename = fileDialog.FileName;
                //MessageBox.Show("Loading file! " + filename);
                TaskList.Clear();
                TaskList.AddRange(ioManager.LoadFile(filename));
            }
            
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

        #endregion



        public void SelectedTask(ToDoTask task)
        {
            if (task != null)
            {
                editedTask = task;
                _eventAggregator.GetEvent<CreateEditTaskEvent>().Publish(task);
            }
        }
    }
}

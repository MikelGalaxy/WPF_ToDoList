using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ToDoList.ClientWPF.Command;
using ToDoList.ClientWPF.Event;

namespace ToDoList.ClientWPF.ViewModel
{
    public class AddTaskViewModel: BaseViewModel, IAddTaskViewModel
    {
        private IEventAggregator _eventAggregator;

        public RelayCommand AddSaveTask { get; set; }
        public RelayCommand Cancel { get; set; }


        #region Properties
        private string _title;

        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }

        private DateTime _dueDate;

        public DateTime DueDate
        {
            get { return _dueDate; }
            set { _dueDate = value; }
        }


        private int _completion;

        public int Completion
        {
            get { return _completion; }
            set { _completion = value;
                OnPropertyChanged();
                }
        }

        private string _description;

        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }
        #endregion


        public AddTaskViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            AddSaveTask = new RelayCommand(OnAddSaveTaskClick, CanAddSaveClick);
            Cancel = new RelayCommand(OnCancelClick, CanCancelClick);
            
        }

       




        #region Commands

        private void OnAddSaveTaskClick(object obj)
        {
            //to change
            _eventAggregator.GetEvent<SendTaskToListEvent>().Publish(null);
        }

        private bool CanAddSaveClick(object obj)
        {
            return true;
        }

        private bool CanCancelClick(object obj)
        {
            return true;
        }

        private void OnCancelClick(object obj)
        {
            _eventAggregator.GetEvent<SendTaskToListEvent>().Publish(null);
        }

        #endregion

        public void LoadTask(Task task)
        {
            if(task!=null)
            {

            }

        }

    }
}

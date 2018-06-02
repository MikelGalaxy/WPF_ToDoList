﻿using Prism.Events;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ToDoList.ClientWPF.Command;
using ToDoList.ClientWPF.Event;
using ToDoList.Model;

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
            set { _title = value;
                OnPropertyChanged();
            }
        }

        private DateTime _dueDate;

        public DateTime DueDate
        {
            get { return _dueDate; }
            set { _dueDate = value;
                OnPropertyChanged();
            }
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
            set { _description = value;
                OnPropertyChanged();
            }
        }
        #endregion

        private string _buttonText="Save";

        public string ButtonText
        {
            get { return _buttonText; }
            set { _buttonText = value;
                OnPropertyChanged();
            }
        }

        private bool edited = false;
        private ToDoTask receivedTask;

        public AddTaskViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            AddSaveTask = new RelayCommand(OnAddSaveTaskClick, CanAddSaveClick);
            Cancel = new RelayCommand(OnCancelClick, CanCancelClick);
            eventAggregator.GetEvent<CreateEditTaskEvent>().Subscribe(LoadTask);
            clearFields();
        }

       




        #region Commands

        private void OnAddSaveTaskClick(object obj)
        {
            //to change
            ToDoTask toDoTask = new ToDoTask(false,DueDate.ToString("yyyy-MM-dd"), Title,Completion,Description);
            if (Completion == 100)
                toDoTask.Checked = true;
            if(edited)
            {
                _eventAggregator.GetEvent<SendEditedTaskToListEvent>().Publish(toDoTask);
                clearFields();
            }
            else
            {
                _eventAggregator.GetEvent<SendTaskToListEvent>().Publish(toDoTask);
                clearFields();
            }           
        }

        private bool CanAddSaveClick(object obj)
        {
            if(edited)
            {
                if (receivedTask.Title != Title || receivedTask.DueDate != DueDate.ToString("yyyy-MM-dd")
                    || receivedTask.Completion != Completion || receivedTask.Description != Description)
                    return true;

            }else
            {
                //adding task
                if (Title.Length > 0 && Description.Length > 0)
                    return true;
            }
            return false;
        }

        private bool CanCancelClick(object obj)
        {
            return true;
        }

        private void OnCancelClick(object obj)
        {
            _eventAggregator.GetEvent<SendTaskToListEvent>().Publish(null);
            clearFields();
        }

        #endregion

        public void LoadTask(ToDoTask task)
        {
            if(task!=null)
            {
                DueDate = DateTime.ParseExact(task.DueDate,"yyyy-MM-dd", new DateTimeFormatInfo());
                Title = task.Title;
                Completion = task.Completion;
                Description = task.Description;
                ButtonText = "Save";
                edited = true;
                receivedTask = task;
            }
            else
            {
                ButtonText = "Add";
                edited = false;
            }
           
        }

        private void clearFields()
        {
            DueDate = DateTime.Now.Date;
            Title = "";
            Completion = 0;
            Description = "";
        }

    }
}

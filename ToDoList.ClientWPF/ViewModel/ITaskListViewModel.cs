﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Model;

namespace ToDoList.ClientWPF.ViewModel
{
    public interface ITaskListViewModel
    {
        void SelectedTask(ToDoTask task);
    }
}

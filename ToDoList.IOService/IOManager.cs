﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Model;

namespace ToDoList.IOService
{
    public class IOManager
    {
        //private string path;
        public IOManager()
        {
            //path = "";
        }

        public IEnumerable<ToDoTask> LoadFile(string path)
        {
            var newList=new List<ToDoTask>();

            string[] fileLines = System.IO.File.ReadAllLines(path);

            foreach (string line in fileLines)
            {
                string[] taskStr = line.Split(',');
                ToDoTask nTask = new ToDoTask(Boolean.Parse(taskStr[0]), taskStr[1], taskStr[2], Int32.Parse(taskStr[3]), taskStr[4]);
                newList.Add(nTask);
                
            }
            return newList;
        }

        public void SaveFile(List<ToDoTask> list,string path)
        {
            int i = 0;
            string[] tasksStr=new string[list.Count];
            foreach (var task in list)
            {
                tasksStr[i] += $"{task.Checked},{task.DueDate},{task.Title},{task.Completion},{task.Description}";
                i ++;
            }

            System.IO.File.WriteAllLines(path, tasksStr);

        }




    }
}

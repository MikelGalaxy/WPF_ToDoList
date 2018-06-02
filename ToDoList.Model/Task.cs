using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList.Model
{
    public class ToDoTask
    {
        public ToDoTask()
        {

        }


        public ToDoTask(bool isChecked,string dueDate,string title,int completion,string description)
        {
            Checked = isChecked;
            DueDate = dueDate;
            Title = title;
            Completion = completion;
            Description = description;
        }


        public bool Checked { get; set; }
        public string DueDate { get; set; }
        public string Title { get; set; }
        public int Completion { get; set; }
        public string Description { get; set; }
    }
}

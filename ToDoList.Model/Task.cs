using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList.Model
{
    public class Task
    {
        public bool Checked { get; set; }
        public DateTime DueDate { get; set; }
        public string Title { get; set; }
        public int Completion { get; set; }
        public string Description { get; set; }
    }
}

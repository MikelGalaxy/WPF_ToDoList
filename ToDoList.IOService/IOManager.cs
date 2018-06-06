using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Model;

namespace ToDoList.IOService
{
    public class IOManager
    {
        private string path;
        public IOManager()
        {
            path = "";
        }

        public IEnumerable<ToDoTask> LoadFile()
        {
            IEnumerable<ToDoTask> newList=new List<ToDoTask>();






            return newList;
        }

        public void SaveFile(List<ToDoTask> list)
        {


        }




    }
}

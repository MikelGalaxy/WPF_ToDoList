using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Model;

namespace ToDoList.ClientWPF.Event
{
    public class SendTaskToListEvent:PubSubEvent<ToDoTask>
    {
    }
}

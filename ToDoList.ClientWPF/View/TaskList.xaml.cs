using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ToDoList.ClientWPF.ViewModel;
using ToDoList.Model;

namespace ToDoList.ClientWPF.View
{
    /// <summary>
    /// Interaction logic for TaskList.xaml
    /// </summary>
    public partial class TaskList : UserControl
    {
        public ITaskListViewModel List { get; set; }
        public TaskList(ITaskListViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
            List = viewModel;
        }

        protected void HandleDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ToDoTask x = ((ListViewItem)sender).Content as ToDoTask;
            List.SelectedTask(x);
        }
    }
}

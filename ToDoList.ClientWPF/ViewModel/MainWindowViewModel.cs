using Microsoft.Practices.Unity;
using Prism.Events;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.ClientWPF.Event;
using ToDoList.ClientWPF.View;

namespace ToDoList.ClientWPF.ViewModel
{
    public class MainWindowViewModel: BaseViewModel, IMainWindowViewModel
    {

        private IEventAggregator _eventAggregator;
        private IRegionManager _regionManager;
        private IUnityContainer _unityContainer;
        public MainWindowViewModel(IEventAggregator eventAggregator, IRegionManager regionManager, IUnityContainer unityContainer)
        {
            _eventAggregator = eventAggregator;
            //Menu = new RelayCommand(x => OnMenuCommand());
            eventAggregator.GetEvent<CreateEditTaskEvent>().Subscribe(switchWindow);
            eventAggregator.GetEvent<SendTaskToListEvent>().Subscribe(backToList);
            _regionManager = regionManager;
            _unityContainer = unityContainer;

           
        }

        private void backToList(Task obj)
        {
            _regionManager.Regions["ContentRegion"].Activate(_unityContainer.Resolve<TaskList>());
        }

        private void switchWindow(Task obj)
        {
            _regionManager.Regions["ContentRegion"].Activate(_unityContainer.Resolve<AddTask>());
        }
    }
}

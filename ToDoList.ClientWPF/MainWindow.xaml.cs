using MahApps.Metro.Controls;
using Microsoft.Practices.Unity;
using Prism.Regions;
using System;

using ToDoList.ClientWPF.View;
using ToDoList.ClientWPF.ViewModel;

namespace ToDoList.ClientWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {

        private IMainWindowViewModel _viewModel;
        private IRegionManager _regionManager;
        //public MainWindow()
        //{
        //    InitializeComponent();
        //}

        public MainWindow(IUnityContainer container, IRegionManager regionManager)
        {
            InitializeComponent();
            _viewModel = container.Resolve<IMainWindowViewModel>();

            RegionManager.SetRegionManager(this, regionManager);
            regionManager.Regions["ContentRegion"].Add(container.Resolve<AddTask>());
            regionManager.Regions["ContentRegion"].Add(container.Resolve<TaskList>());

            regionManager.Regions["ContentRegion"].Activate(container.Resolve<TaskList>());
            DataContext = _viewModel;
            _regionManager = regionManager;
        }
    }
}

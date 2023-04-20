using AMS.Store;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMS.ViewModels
{
    public class MainViewModel:ViewModelBase
    {

        public NavigationStore navigationStore;

        public ViewModelBase CurrentViewModel => navigationStore.CurrentViewModel;

        public MainViewModel(NavigationStore navigationstore)
        {
            navigationStore = navigationstore;

            navigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;
        }

        private void OnCurrentViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentViewModel));
        }

    }
}

using AMS.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMS.Store
{
    public class InternalMenuNavigationStore
    {
        public event Action? CurrentSelectedFeatureViewModelChanged;

        private ViewModelBase currentSelectedFeatureViewModel;

        public ViewModelBase CurrentSelectedFeatureViewModel
        {
            get
            {
                return currentSelectedFeatureViewModel;
            }

            set
            {
                currentSelectedFeatureViewModel = value;

                OnCurrentSelectedFeatureViewModelChanged();
            }
        }

        private void OnCurrentSelectedFeatureViewModelChanged()
        {
            CurrentSelectedFeatureViewModelChanged?.Invoke();
        }
    }
}

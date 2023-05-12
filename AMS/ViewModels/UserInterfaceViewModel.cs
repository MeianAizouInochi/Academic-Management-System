using AMS.Commands;
using AMS.Models;
using AMS.Store;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AMS.ViewModels
{
    public class UserInterfaceViewModel:ViewModelBase
    {

        public IUserInterfaceSideNavigationViewModel userInterfaceSideNavigationViewModel { get; set; }

        public InternalMenuNavigationStore internalMenuNavigationStore;

        public ViewModelBase CurrentSelectedFeatureViewModel => internalMenuNavigationStore.CurrentSelectedFeatureViewModel;

        public UserInterfaceViewModel(InternalMenuNavigationStore internalmenuNavigationStore, UserDetails basicUserDetails)
        {

            internalMenuNavigationStore = internalmenuNavigationStore;

            if (basicUserDetails is StudentUserDetails)
            {
                userInterfaceSideNavigationViewModel = new UserInterfaceStudentTypeSideNavigation(internalmenuNavigationStore,(StudentUserDetails)basicUserDetails);

            }
            else 
            {
                userInterfaceSideNavigationViewModel = new UserInterfaceOfficialTypeSideNavigation(internalmenuNavigationStore, (OfficialUserDetails)basicUserDetails);
            }

            internalMenuNavigationStore.CurrentSelectedFeatureViewModelChanged += OnCurrentSelectedFeatureViewModelChanged;
        }

        private void OnCurrentSelectedFeatureViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentSelectedFeatureViewModel));
        }
    }
}

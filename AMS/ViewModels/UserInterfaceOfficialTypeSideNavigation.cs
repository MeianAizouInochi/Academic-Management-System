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
    public class UserInterfaceOfficialTypeSideNavigation : ViewModelBase, IUserInterfaceSideNavigationViewModel
    {
        public ICommand? NavigateToDashBoardCommand { get; }

        public ICommand? NavigateToAttendanceMarkerCommand { get; }

        public UserInterfaceOfficialTypeSideNavigation(InternalMenuNavigationStore internalMenuNavigationStore, OfficialUserDetails officialUserDetails)
        {
            NavigateToDashBoardCommand = new NavigationToDashboardCommand(internalMenuNavigationStore, officialUserDetails);

            NavigateToAttendanceMarkerCommand = new NavigationToOfficialAttendanceMarkerCommand(internalMenuNavigationStore);

        }
    }
}

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
    public class UserInterfaceStudentTypeSideNavigation : ViewModelBase, IUserInterfaceSideNavigationViewModel
    {
        public ICommand? NavigateToDashBoardCommand { get; }

        public ICommand? NavigateToStudentTimeTableCommand { get; }


        public UserInterfaceStudentTypeSideNavigation(InternalMenuNavigationStore internalMenuNavigationStore, StudentUserDetails studentUserDetails)
        {

            NavigateToDashBoardCommand = new NavigationToDashboardCommand(internalMenuNavigationStore, studentUserDetails);
            
        }
    }
}

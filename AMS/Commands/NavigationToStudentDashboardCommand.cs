using AMS.Models;
using AMS.Store;
using AMS.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMS.Commands
{
    public class NavigationToStudentDashboardCommand:CommandBase
    {
        InternalMenuNavigationStore internalMenuNavigationStore;

        BasicUserDetails _basicUserDetails;

        public NavigationToStudentDashboardCommand(InternalMenuNavigationStore internalMenuNavigationstore,BasicUserDetails basicUserDetails)
        {
            internalMenuNavigationStore = internalMenuNavigationstore;
            _basicUserDetails = basicUserDetails;
        }

        public override void Execute(object? parameter)
        {
            internalMenuNavigationStore.CurrentSelectedFeatureViewModel = new UserDashboardViewModel(_basicUserDetails);
        }
    }
}

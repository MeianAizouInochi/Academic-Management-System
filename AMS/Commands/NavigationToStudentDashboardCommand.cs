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

        public NavigationToStudentDashboardCommand(InternalMenuNavigationStore internalMenuNavigationstore)
        {
            internalMenuNavigationStore = internalMenuNavigationstore;
        }

        public override void Execute(object? parameter)
        {
            internalMenuNavigationStore.CurrentSelectedFeatureViewModel = new UserDashboardViewModel();
        }
    }
}

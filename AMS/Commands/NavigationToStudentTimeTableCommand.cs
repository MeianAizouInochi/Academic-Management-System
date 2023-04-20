using AMS.Store;
using AMS.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMS.Commands
{
    public class NavigationToStudentTimeTableCommand:CommandBase
    {
        InternalMenuNavigationStore internalMenuNavigationStore;

        public NavigationToStudentTimeTableCommand(InternalMenuNavigationStore internalMenuNavigationstore)
        {
            internalMenuNavigationStore = internalMenuNavigationstore;
        }

        public override void Execute(object? parameter)
        {
            internalMenuNavigationStore.CurrentSelectedFeatureViewModel = new UserTimeTableViewModel();
        }
    }
}

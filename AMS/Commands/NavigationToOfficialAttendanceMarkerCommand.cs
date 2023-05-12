using AMS.Store;
using AMS.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMS.Commands
{
    public class NavigationToOfficialAttendanceMarkerCommand : CommandBase
    {

        InternalMenuNavigationStore internalmenuNavigationStore;

        public NavigationToOfficialAttendanceMarkerCommand(InternalMenuNavigationStore internalMenuNavigationStore)
        {
            internalmenuNavigationStore = internalMenuNavigationStore;
        }

        public override void Execute(object? parameter)
        {
            internalmenuNavigationStore.CurrentSelectedFeatureViewModel = new OfficialAttendanceMarkerViewModel();
        }
    }
}

using AMS.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMS.Commands
{
    public class ChangeUserDataCommand : CommandBase
    {
        ObservableCollection<UserDashboardProfileInfoDetailsListItemViewModel> _profiles;

        public ChangeUserDataCommand(ObservableCollection<UserDashboardProfileInfoDetailsListItemViewModel> userDashboardProfileInfoDetailsList) 
        {
            _profiles = userDashboardProfileInfoDetailsList;
        }

        public override void Execute(object? parameter)
        {
            //TODO : Logic for changing the data.
        }
    }
}

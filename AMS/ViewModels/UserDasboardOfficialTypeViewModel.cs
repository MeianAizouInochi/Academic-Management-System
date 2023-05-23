using AMS.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMS.ViewModels
{
    public class UserDasboardOfficialTypeViewModel : ViewModelBase, IUserTypeDashboardInterface
    {
        public string imagepath 
        { 
            get;
            set;
        }

        public ObservableCollection<UserDashboardProfileInfoDetailsListItemViewModel> UserDashboardProfileInfoDetailsList { get; set; }


        public UserDasboardOfficialTypeViewModel(OfficialUserDetails officialUserDetails)
        {
            imagepath = officialUserDetails.path;
        }
    }
}

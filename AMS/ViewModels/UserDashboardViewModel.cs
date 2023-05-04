using AMS.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMS.ViewModels
{
    public class UserDashboardViewModel:ViewModelBase
    {
        //private ObservableCollection<UserDashboardProfileInfoDetailsListItemViewModel> userDashboardProfileInfoDetailsList;

        //public ObservableCollection<UserDashboardProfileInfoDetailsListItemViewModel> UserDashboardProfileInfoDetailsLists 
        //{
        //    get 
        //    {
        //        return userDashboardProfileInfoDetailsList;
        //    }
        //    set 
        //    { 
        //        userDashboardProfileInfoDetailsList = value;
        //    }
        //}

        private string Name = "";

        public string NameText
        {
            get
            {
                return Name;
            }

            set
            {
                OnPropertyChanged(nameof(NameText));
            }
        }



        public UserDashboardViewModel(BasicUserDetails basicUserDetails)
        {
            // TODO: Set the details from basicUserDetails to UI

            NameText = basicUserDetails.UserName;
        }



    }
}

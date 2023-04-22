using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMS.ViewModels
{
    public class UserDashboardProfileInfoDetailsListItemViewModel:ViewModelBase
    {
        public string InfoType 
        { 
            get;
            set; 
        }

        private string info;

        public string Info 
        {
            get 
            {
                return info;
            }

            set 
            {
                info = value;
                OnPropertyChanged(nameof(Info));
            } 
        }

        public UserDashboardProfileInfoDetailsListItemViewModel( string _infoType, string _info)
        {
            this.InfoType = _infoType;

            this.Info = _info;
        }

    }
}

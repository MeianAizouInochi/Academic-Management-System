using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMS.ViewModels
{
    public class OfficialAttendanceMarkerViewModel:ViewModelBase
    {
        private string Hello;

        public string bindCheck 
        {
            get { return Hello; }
            set { Hello = value;
                OnPropertyChanged(nameof(bindCheck));
            }
        }

        public OfficialAttendanceMarkerViewModel()
        {
            bindCheck = "It Works";
        }
    }
}

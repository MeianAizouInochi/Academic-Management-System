using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMS.ViewModels
{
    public class AttendanceMarkerItemTemplateViewModel:ViewModelBase
    {
        public string Id { get; set; }

        private bool attended;

        public bool Attended
        {
            get { return attended; }
            set { attended = value; OnPropertyChanged(nameof(Attended)); }
        }


        public AttendanceMarkerItemTemplateViewModel(string id, bool CheckBoxValue)
        {
            Id = id;

            Attended = CheckBoxValue;
        }
    }
}

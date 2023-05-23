using AMS.AMSExceptions;
using AMS.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AMS.Commands
{
    public class AttendanceSubmittionCommand : CommandBase
    {
        OfficialAttendanceMarkerViewModel _attendancemarkerViewModel;

        public AttendanceSubmittionCommand(OfficialAttendanceMarkerViewModel officialAttendanceMarkerViewModel)
        {
            _attendancemarkerViewModel = officialAttendanceMarkerViewModel;
        }

        public override void Execute(object? parameter)
        {
            try
            {
                // TODO: Perform Submittion Operation

                //After the operation is successfully performed.

                _attendancemarkerViewModel.StudentRollList = null;

                _attendancemarkerViewModel.AttendanceMarkerVisibility = !_attendancemarkerViewModel.AttendanceMarkerVisibility;

                _attendancemarkerViewModel.MarkAttendanceButtonTitle = "Mark Attendance!";
            }
            catch (AMSError_Exceptions er) 
            {
                MessageBox.Show(er.Message);
            }


        }
    }
}

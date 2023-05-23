using AMS.AMSExceptions;
using AMS.Models;
using AMS.Models.DataAccessTemp;
using AMS.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AMS.Commands
{
    public class AttendanceMarkerVisibilityChangerCommand : CommandBase
    {
        OfficialAttendanceMarkerViewModel _attendanceMarkerViewModel;

        public AttendanceMarkerVisibilityChangerCommand(OfficialAttendanceMarkerViewModel attendanceMarkerViewModel)
        {
            _attendanceMarkerViewModel = attendanceMarkerViewModel;
        }

        public override void Execute(object? parameter)
        {
            try
            {
                if (!_attendanceMarkerViewModel.AttendanceMarkerVisibility)
                {
                    AwsDataAccessUserDetails awsDataAccessUserDetails = new AwsDataAccessUserDetails();

                    StudentList studentList = new StudentList();

                    awsDataAccessUserDetails.GetStudentIDList
                        (
                        studentList,
                        _attendanceMarkerViewModel.BatchValue,
                        _attendanceMarkerViewModel.CourseValue,
                        _attendanceMarkerViewModel.BranchValue,
                        _attendanceMarkerViewModel.SectionValue
                        );

                    _attendanceMarkerViewModel.StudentRollList = studentList;

                }

                _attendanceMarkerViewModel.AttendanceMarkerVisibility = !_attendanceMarkerViewModel.AttendanceMarkerVisibility;

                _attendanceMarkerViewModel.MarkAttendanceButtonTitle = _attendanceMarkerViewModel.AttendanceMarkerVisibility ? "Cancel Marking Attendance!" : "Mark Attendance!";
            }
            catch (AMSError_Exceptions er) 
            {
                MessageBox.Show(er.Message);
            }
            

            
        }
    }
}

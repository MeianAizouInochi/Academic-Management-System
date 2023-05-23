using AMS.AMSExceptions;
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
                // TODO: Secure Submittion Operation within 1 Transaction.
                // It would be bad, if only 1 transaction runs and the other fails.
                // It should be all or none.

                string[] TableInfo = new string[]
                {
                    _attendancemarkerViewModel.BatchValue,
                    _attendancemarkerViewModel.CourseValue,
                    _attendancemarkerViewModel.BranchValue,
                    _attendancemarkerViewModel.SectionValue
                };

                List<string> PresentStudents = new List<string>();

                List<string> AbsentStudents = new List<string>();

                foreach (AttendanceMarkerItemTemplateViewModel obj in _attendancemarkerViewModel.AttendanceList) // TODO: Solve the null value reference possibility warning
                {
                    if (obj.Attended == true)
                    {
                        PresentStudents.Add(obj.Id);
                    }
                    else 
                    {
                        AbsentStudents.Add(obj.Id);
                    }
                }

                AwsDataAccessUserDetails awsDataAccessUserDetails = new AwsDataAccessUserDetails();

                if (PresentStudents.Count >= 1)
                {
                    awsDataAccessUserDetails.UpdateStudentAttendance
                    (
                    PresentStudents.ToArray(),
                    TableInfo,
                    "10",
                    _attendancemarkerViewModel.SubjectValue,
                    "1",
                    "1"
                    );

                }

                if (AbsentStudents.Count >= 1)
                {
                    awsDataAccessUserDetails.UpdateStudentAttendance
                    (
                    AbsentStudents.ToArray(),
                    TableInfo,
                    "10",
                    _attendancemarkerViewModel.SubjectValue,
                    "0",
                    "1"
                    );
                }


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

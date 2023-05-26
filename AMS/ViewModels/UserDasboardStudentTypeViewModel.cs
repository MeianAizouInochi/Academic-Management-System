using AMS.Models;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMS.ViewModels
{
    public class UserDasboardStudentTypeViewModel : ViewModelBase, IUserTypeDashboardInterface
    {

        // TODO: Create a listener, which listens whenever the data of the user changes in dynamodb,
        // and retrieves the new data and calls a function to set the new data into the properties,
        // so that it shows in the UI at Real Time.

        public string imagepath { get; set; }

        public string AttendanceGridTitle { get; set; } = "Personal Attendance Chart";

        public string InfoFromTeacher { get; set; } = "Message from Teacher";

        private ISeries[] attendancePercentageData;

        public ISeries[] AttendancePercentageData
        {
            get { return attendancePercentageData; }

            set
            {
                attendancePercentageData = value;

                OnPropertyChanged(nameof(AttendancePercentageData));
            }
        }

        private Axis[] attendanceXaxisInfo;

        public Axis[] AttendanceXaxisInfo
        {
            get { return attendanceXaxisInfo; }
            set
            {
                attendanceXaxisInfo = value;

                OnPropertyChanged(nameof(AttendanceXaxisInfo));
            }
        }

        private ObservableCollection<UserDashboardProfileInfoDetailsListItemViewModel> userDashboardProfileInfoDetailsList;

        public ObservableCollection<UserDashboardProfileInfoDetailsListItemViewModel> UserDashboardProfileInfoDetailsList
        {
            get
            {
                return userDashboardProfileInfoDetailsList;
            }
            set
            {
                userDashboardProfileInfoDetailsList = value;
                OnPropertyChanged(nameof(UserDashboardProfileInfoDetailsList));
            }
        }

        // TODO: Null Check Required for the parameter passed to the constructor. 
        public UserDasboardStudentTypeViewModel(StudentUserDetails st_userDetails)
        {
            UserDashboardProfileInfoDetailsList = new ObservableCollection<UserDashboardProfileInfoDetailsListItemViewModel>
            {
                new UserDashboardProfileInfoDetailsListItemViewModel("Id:", st_userDetails.UserName),
                new UserDashboardProfileInfoDetailsListItemViewModel("Name:", st_userDetails.Name),
                new UserDashboardProfileInfoDetailsListItemViewModel("Sem:", st_userDetails.Semester),
                new UserDashboardProfileInfoDetailsListItemViewModel("Email:", st_userDetails.Email),
                new UserDashboardProfileInfoDetailsListItemViewModel("Mobile No':", st_userDetails.MobileNumber),
                new UserDashboardProfileInfoDetailsListItemViewModel("Nationality:", st_userDetails.Nationality),
                new UserDashboardProfileInfoDetailsListItemViewModel("Address:", st_userDetails.HomeAddress),
                new UserDashboardProfileInfoDetailsListItemViewModel("Hostel:", st_userDetails.HostelFacilityAvailed),
                new UserDashboardProfileInfoDetailsListItemViewModel("BloodType:", st_userDetails.BloodType),
                new UserDashboardProfileInfoDetailsListItemViewModel("Age:", st_userDetails.Age),
                new UserDashboardProfileInfoDetailsListItemViewModel("Batch:", st_userDetails.Batch),
                new UserDashboardProfileInfoDetailsListItemViewModel("Course:", st_userDetails.Course),
                new UserDashboardProfileInfoDetailsListItemViewModel("Branch:", st_userDetails.Branch),
                new UserDashboardProfileInfoDetailsListItemViewModel("Batch:", st_userDetails.Section)
            };

            /*-----Protected Code below, dont touch!------*/

            // TODO: Needs Optimization.
            // TODO: Needs implementation of error handling system, after optimization.

            string AttendanceRecordData = st_userDetails.AttendanceRecord;

            string[] SubjectWiseData = AttendanceRecordData.Split(',');

            List<string[]> AttendanceRecordsDefinedData = new List<string[]>();

            foreach (string i in SubjectWiseData)
            {
                string temp;
                temp = i.Replace("[", "");
                temp = temp.Replace("]","");

                string[] temparr = temp.Split(":");

                AttendanceRecordsDefinedData.Add(temparr);
            }

      
            List<double> AttendanceNumericData = new  List<double>();

            foreach (string[] a in AttendanceRecordsDefinedData) 
            {
                AttendanceNumericData.Add(Convert.ToDouble(a[1]) / Convert.ToDouble(a[2]) * 100);
            }

            List<string> SubjectNames = new List<string>();

            foreach (string[] a in AttendanceRecordsDefinedData)
            {
                SubjectNames.Add(a[0]);
            }

            /*-----Protected Code above, dont touch!------*/

            AttendancePercentageData = new ISeries[] {
                new ColumnSeries<double> {
                    Name = st_userDetails.Name,
                    Values= AttendanceNumericData // TODO: make this implement Inotifypropertychanged
                } };

            AttendanceXaxisInfo = new Axis[] {
                new Axis {
                    Labels = SubjectNames,// TODO: make this implement Inotifypropertychanged
                    SeparatorsPaint = new SolidColorPaint(new SKColor(200,200,200)),
                    SeparatorsAtCenter = false,
                    TicksPaint = new SolidColorPaint(new SKColor(35,35,35)),
                    TicksAtCenter = true
                } };

            imagepath = st_userDetails.path;
        }
    }
}

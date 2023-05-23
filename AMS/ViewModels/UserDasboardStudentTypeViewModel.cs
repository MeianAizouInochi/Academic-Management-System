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
                new UserDashboardProfileInfoDetailsListItemViewModel("Course:", st_userDetails.Course),
                new UserDashboardProfileInfoDetailsListItemViewModel("Branch:", st_userDetails.Branch),
                new UserDashboardProfileInfoDetailsListItemViewModel("Batch:", st_userDetails.Batch),
                new UserDashboardProfileInfoDetailsListItemViewModel("Email:", st_userDetails.Email),
                new UserDashboardProfileInfoDetailsListItemViewModel("Mobile No':", st_userDetails.MobileNumber),
                new UserDashboardProfileInfoDetailsListItemViewModel("Nationality:", st_userDetails.Nationality),
                new UserDashboardProfileInfoDetailsListItemViewModel("Address:", st_userDetails.HomeAddress),
                new UserDashboardProfileInfoDetailsListItemViewModel("Hostel:", st_userDetails.HostelFacilityAvailed),
                new UserDashboardProfileInfoDetailsListItemViewModel("BloodType:", st_userDetails.BloodType)

            };

            //ChangeListData = new ChangeUserDataCommand(UserDashboardProfileInfoDetailsLists);

            AttendancePercentageData = new ISeries[] {
                new ColumnSeries<double> {
                    Name = st_userDetails.Name,
                    Values= new double[]{ 75,50,65.6,70, 55.5} // TODO: Change this data to accept from aws. Attendance Percentage
                } };

            AttendanceXaxisInfo = new Axis[] {
                new Axis {
                    Labels = new string[]{ "CC", "CD", "AI", "MPMC", "DS" },// TODO: Change this data to accept from aws. Subjects wrt attendance
                    SeparatorsPaint = new SolidColorPaint(new SKColor(200,200,200)),
                    SeparatorsAtCenter = false,
                    TicksPaint = new SolidColorPaint(new SKColor(35,35,35)),
                    TicksAtCenter = true
                } };

            imagepath = st_userDetails.path;
        }
    }
}

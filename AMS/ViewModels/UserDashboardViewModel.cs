using AMS.Commands;
using AMS.Models;
using DocumentFormat.OpenXml.Drawing.Charts;
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
using System.Windows.Input;

namespace AMS.ViewModels
{
    public class UserDashboardViewModel:ViewModelBase
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

        public ObservableCollection<UserDashboardProfileInfoDetailsListItemViewModel> UserDashboardProfileInfoDetailsLists
        {
            get
            {
                return userDashboardProfileInfoDetailsList;
            }
            set
            {
                userDashboardProfileInfoDetailsList = value;
                OnPropertyChanged(nameof(UserDashboardProfileInfoDetailsLists));
            }
        }

        public ICommand ChangeListData { get; }

        
        public UserDashboardViewModel(BasicUserDetails basicUserDetails)
        {

            UserDashboardProfileInfoDetailsLists = new ObservableCollection<UserDashboardProfileInfoDetailsListItemViewModel>
            {
                new UserDashboardProfileInfoDetailsListItemViewModel("Id:", basicUserDetails.UserName),
                new UserDashboardProfileInfoDetailsListItemViewModel("Name:", basicUserDetails.Name),
                new UserDashboardProfileInfoDetailsListItemViewModel("Sem:", basicUserDetails.Semester),
                new UserDashboardProfileInfoDetailsListItemViewModel("Course:", basicUserDetails.Course),
                new UserDashboardProfileInfoDetailsListItemViewModel("Branch:", basicUserDetails.Branch),
                new UserDashboardProfileInfoDetailsListItemViewModel("Batch:", basicUserDetails.Batch),
                new UserDashboardProfileInfoDetailsListItemViewModel("Email:", basicUserDetails.Email),
                new UserDashboardProfileInfoDetailsListItemViewModel("Mobile No':", basicUserDetails.MobileNumber),
                new UserDashboardProfileInfoDetailsListItemViewModel("Nationality:", basicUserDetails.Nationality),
                new UserDashboardProfileInfoDetailsListItemViewModel("Address:", basicUserDetails.HomeAddress),
                new UserDashboardProfileInfoDetailsListItemViewModel("Hostel:", basicUserDetails.Hostel),
                new UserDashboardProfileInfoDetailsListItemViewModel("BloodType:", basicUserDetails.BloodType)
       
            };

            ChangeListData = new ChangeUserDataCommand(UserDashboardProfileInfoDetailsLists);

            AttendancePercentageData = new ISeries[] { 
                new ColumnSeries<double> { 
                    Name = basicUserDetails.Name,
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

            imagepath = basicUserDetails.path;
        }

    }
}

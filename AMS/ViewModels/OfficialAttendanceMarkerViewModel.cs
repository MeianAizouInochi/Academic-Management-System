using AMS.Commands;
using AMS.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AMS.ViewModels
{
    public class OfficialAttendanceMarkerViewModel:ViewModelBase
    {
        public string ViewTitle { get; set; }

        public string BatchTitle { get; set; }

        public string CourseTitle { get; set; }

        public string BranchTitle { get; set; }

        public string SectionTitle { get; set; }

        public string SubjectTitle { get; set; }

        public string SubmitAttendanceButtonTitle { get; set; }

        private string markAttendanceButtonTitle = "";

        public string MarkAttendanceButtonTitle
        {
            get { return markAttendanceButtonTitle; }
            set { markAttendanceButtonTitle = value; OnPropertyChanged(nameof(MarkAttendanceButtonTitle)); }
        }


        private string batchValue="";

        public string BatchValue
        {
            get { return batchValue; }
            set { batchValue = value; OnPropertyChanged(nameof(BatchValue)); }
        }

        private string courseValue="";

        public string CourseValue
        {
            get { return courseValue; }
            set { courseValue = value; OnPropertyChanged(nameof(CourseValue)); }
        }

        private string branchValue = "";

        public string BranchValue
        {
            get { return branchValue; }
            set { branchValue = value; OnPropertyChanged(nameof(BranchValue)); }
        }
        private string sectionValue = "";

        public string SectionValue
        {
            get { return sectionValue; }
            set { sectionValue = value; OnPropertyChanged(nameof(SectionValue)); }
        }

        private string subjectValue = "";

        public string SubjectValue
        {
            get { return subjectValue; }
            set { subjectValue = value; OnPropertyChanged(nameof(SubjectValue)); }
        }

        private bool attendanceMarkerVisibility = false;
                
        public bool AttendanceMarkerVisibility
        {
            get { return attendanceMarkerVisibility; }
            set { attendanceMarkerVisibility = value; OnPropertyChanged(nameof(AttendanceMarkerVisibility)); }
        }

        private ObservableCollection<AttendanceMarkerItemTemplateViewModel>? _attendanceList = null;

        public ObservableCollection<AttendanceMarkerItemTemplateViewModel>? AttendanceList
        {
            get { return _attendanceList; }
            set { _attendanceList = value; OnPropertyChanged(nameof(AttendanceList)); }
        }

        private StudentList? studentRollList;

        public StudentList? StudentRollList
        {
            get 
            { 
                return studentRollList;
            }
            set 
            { 
                studentRollList = value;

                if (studentRollList != null)
                {
                    PopulateListViewItems();
                }
                else 
                {
                    DePopulateListViewItems();
                }

                OnPropertyChanged(nameof(StudentRollList));
            }
        }

        private bool studentIdListIsEmpty = true;

        public bool StudentIdListIsEmpty
        {
            get { return studentIdListIsEmpty; }
            set 
            { 
                studentIdListIsEmpty = value;
                OnPropertyChanged(nameof(StudentIdListIsEmpty));
            }
        }


        public ICommand ChangeAttendanceMarkerVisibilityCommand { get; }

        public ICommand SubmitAttendanceCommand { get; }

        private void DePopulateListViewItems()
        {
            AttendanceList = null;

            StudentIdListIsEmpty = true;
        }

        private void PopulateListViewItems()
        {
            if (StudentRollList?.StudentIdList != null)
            {
                if (StudentRollList?.StudentIdList?.Length == 2)
                {
                    StudentIdListIsEmpty = true;
                }
                else 
                {
                    StudentIdListIsEmpty = false;

                    AttendanceList = new ObservableCollection<AttendanceMarkerItemTemplateViewModel>();

                    for (int index = 2; index < StudentRollList?.StudentIdList?.Length; index++)
                    {
                        AttendanceList.Add(new AttendanceMarkerItemTemplateViewModel(StudentRollList?.StudentIdList[index],false));
                    }

                }

            }
        }       

        public OfficialAttendanceMarkerViewModel()
        {
            ViewTitle = "Attendance Marker";

            BatchTitle = "Batch : ";

            CourseTitle = "Course : ";

            BranchTitle = "Branch : ";

            SectionTitle = "Section : ";

            SubjectTitle = "Subject Code : ";

            MarkAttendanceButtonTitle = "Mark Attendance!";

            SubmitAttendanceButtonTitle = "Submit Attendance!";

            ChangeAttendanceMarkerVisibilityCommand = new AttendanceMarkerVisibilityChangerCommand(this);

            SubmitAttendanceCommand = new AttendanceSubmittionCommand(this);
        }
    }
}

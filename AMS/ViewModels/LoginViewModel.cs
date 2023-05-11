using AMS.Store;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using AMS.Commands;

namespace AMS.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        private readonly IEnumerable<string> userTypes;

        public IEnumerable<string> UserTypes => userTypes;


        private string selectedUserType;

        public string SelectedUserType 
        {
            get { return selectedUserType; }
            set 
            { 
                selectedUserType = value;
                OnPropertyChanged(nameof(SelectedUserType));
            }
        }

        private string batch;

        public string Batch
        {
            get { return batch; }
            set
            {
                batch = value;
                OnPropertyChanged(nameof(Batch));
            }
        }

        private string batchlabel;

        public string BatchLabel
        {
            get { return batchlabel; }
            set
            {
                batchlabel = value;
                OnPropertyChanged(nameof(BatchLabel));
            }
        }

        private string branch;

        public string Branch
        {
            get { return branch; }
            set
            {
                branch = value;
                OnPropertyChanged(nameof(Branch));
            }
        }

        private string branchlabel;

        public string BranchLabel
        {
            get { return branchlabel; }
            set
            {
                branchlabel = value;
                OnPropertyChanged(nameof(BranchLabel));
            }
        }

        private string course;

        public string Course
        {
            get { return course; }
            set
            {
                course = value;
                OnPropertyChanged(nameof(Course));
            }
        }

        private string courselabel;

        public string CourseLabel
        {
            get { return courselabel; }
            set
            {
                courselabel = value;
                OnPropertyChanged(nameof(CourseLabel));
            }
        }

        private string sectionlabel;

        public string SectionLabel
        {
            get
            { return sectionlabel; }
            set 
            {
                sectionlabel = value;

                OnPropertyChanged(nameof(SectionLabel));
            }
        }

        private string section;

        public string Section
        {
            get
            { return section; }
            set
            {
                section = value;

                OnPropertyChanged(nameof(Section));
            }
        }

        private string username = "";

        public string Username
        {
            get
            {
                return username;
            }

            set
            {
                if (!(username.Equals(value)))
                {
                    username = value;

                    OnPropertyChanged(nameof(Username));
                }

            }
        }

        private string password = "";

        public string Password
        {
            get
            {
                return password;
            }

            set
            {
                password = value;

            }
        }
        public ICommand LoginCommand { get; }

        public LoginViewModel(NavigationStore navigationstore)
        {
            LoginCommand = new NavigationToUserTypeCommand(this, navigationstore);

            userTypes = new List<string>() { "Student", "Official" }; //Initializing the Shown values in the Combo Box.

            BatchLabel = "Batch";
            CourseLabel = "Course";
            BranchLabel = "Branch";
            SectionLabel = "Section";

        }
    }
}

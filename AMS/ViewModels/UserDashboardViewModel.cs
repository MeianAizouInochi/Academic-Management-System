using AMS.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMS.ViewModels
{
    public class UserDashboardViewModel:ViewModelBase
    {
        //private ObservableCollection<UserDashboardProfileInfoDetailsListItemViewModel> userDashboardProfileInfoDetailsList;

        //public ObservableCollection<UserDashboardProfileInfoDetailsListItemViewModel> UserDashboardProfileInfoDetailsLists 
        //{
        //    get 
        //    {
        //        return userDashboardProfileInfoDetailsList;
        //    }
        //    set 
        //    { 
        //        userDashboardProfileInfoDetailsList = value;
        //    }
        //}

        private string? UserName;

        public string userName 
        { get { return UserName; } set { UserName = value; OnPropertyChanged(nameof(userName)); } }
        
        private string? BloodType;
        public string bloodType { get { return BloodType; } set { BloodType = value; OnPropertyChanged(nameof(bloodType)) ; } }
        
        private string? Email;
        public string email { get { return Email; } set { Email = value; OnPropertyChanged(nameof(email)); } }
        
        private string? Course;
        public string course { get { return Course; } set { Course = value; OnPropertyChanged(nameof(course)); } }
        
        private string? HomeAddress;
        public string homeAddress { get { return HomeAddress; } set { HomeAddress = value; OnPropertyChanged(nameof(homeAddress)); } }
        
        private string? Hostel;

        public string hostel { get { return Hostel; } set { Hostel = value; OnPropertyChanged(nameof(hostel)); } }

        private string? MobileNumber;
        public string mobileNumber { get { return MobileNumber; } set { MobileNumber = value; OnPropertyChanged(nameof(mobileNumber)); } }

        private string? Name;
        public string name { get { return Name; } set { Name = value; OnPropertyChanged(nameof(name)); } }

        private string? Nationality;
        public string nationality { get { return Nationality; } set { Nationality = value; OnPropertyChanged(nameof(nationality)); } }
        
        private string? Semester;
        public string semester { get { return Semester; } set { Semester = value; OnPropertyChanged(nameof(semester)); } }
        
        private string? Password;
        public string password { get { return Password; } set { Password = value; OnPropertyChanged(nameof(Password)); } }
        
        private string? UserType;

        public string userType { get { return UserType; } set { UserType = value; OnPropertyChanged(nameof(userType)); } }
        private string? Batch;
        public string batch { get { return Batch; } set { Batch = value; OnPropertyChanged(nameof(batch)); } }

        private string? Branch;
        public string branch { get { return Branch; } set { Branch = value; OnPropertyChanged(nameof(branch)); } }

        public UserDashboardViewModel(BasicUserDetails basicUserDetails)
        {
            // TODO: Set the details from basicUserDetails to UI

            userName = basicUserDetails.UserName;
            name = basicUserDetails.Name;   
            bloodType = basicUserDetails.BloodType;
            email = basicUserDetails.Email; 
            password = basicUserDetails.Password;    
            batch = basicUserDetails.Batch; 
            userType = basicUserDetails.UserType;   
            nationality = basicUserDetails.Nationality; 
            semester = basicUserDetails.Semester;   
            mobileNumber  = basicUserDetails.MobileNumber;    
            hostel  = basicUserDetails.Hostel;  
            homeAddress = basicUserDetails.HomeAddress; 
            course  = basicUserDetails.Course;
            branch = basicUserDetails.Branch;

        }



    }
}

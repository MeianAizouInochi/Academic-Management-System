using AMS.Commands;
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
using System.Windows.Input;

namespace AMS.ViewModels
{
    public class UserDashboardViewModel:ViewModelBase
    {
        public IUserTypeDashboardInterface userTypeDashboardInterface { get; set; }

        public ICommand ChangeListData { get; }

        
        public UserDashboardViewModel(UserDetails userDetails)
        {

            if (userDetails is StudentUserDetails)
            {
                userTypeDashboardInterface = new UserDasboardStudentTypeViewModel((StudentUserDetails)userDetails);
            }
            else 
            {
                userTypeDashboardInterface = new UserDasboardOfficialTypeViewModel();
            }


            
        }

    }
}

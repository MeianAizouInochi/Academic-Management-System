using AMS.AMSExceptions;
using AMS.Models;
using AMS.Models.DataAccessTemp;
using AMS.Store;
using AMS.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AMS.Commands
{
    public class NavigationToUserTypeCommand:CommandBase
    {
        private LoginViewModel loginViewModel;

        private NavigationStore navigationStore;

        public NavigationToUserTypeCommand(LoginViewModel loginviewmodel, NavigationStore navigationstore)
        {
            loginViewModel = loginviewmodel;

            navigationStore = navigationstore;
        }

        public override void Execute(object? parameter)
        {
            /*Perform Temporary Verification*/

            AwsDataAccessUserDetails awsDataAccessUserDetails;

            //Populating the AwsDataAccessUserDetails object as per the Role of the User.

            if (loginViewModel.SelectedUserType.Equals("Student"))
            {
                awsDataAccessUserDetails = new AwsDataAccessUserDetails(
                    new StudentUserDetails(),
                    loginViewModel.Username,
                    loginViewModel.Password,
                    loginViewModel.Batch,
                    loginViewModel.Course,
                    loginViewModel.Branch,
                    loginViewModel.Section
                    );
            }
            else 
            {
                // TODO: Need to understand what data differentiates different Staff.
                awsDataAccessUserDetails = new AwsDataAccessUserDetails(
                    new OfficialUserDetails(),
                    loginViewModel.Username,
                    loginViewModel.Password
                    );
            }

            try
            {
                //Getting the User Data.
                awsDataAccessUserDetails.GetLoggedInUserDetails();

                InternalMenuNavigationStore internalMenuNavigationStore = new InternalMenuNavigationStore();

                //TODO: Fix the null Object issue for DataObject.
                internalMenuNavigationStore.CurrentSelectedFeatureViewModel = new UserDashboardViewModel(awsDataAccessUserDetails.DataObject);

                navigationStore.CurrentViewModel = new UserInterfaceViewModel(internalMenuNavigationStore,awsDataAccessUserDetails.DataObject);

            }
            catch (AMSError_Exceptions er) { MessageBox.Show(er.Message); }
            
        }
    }
}

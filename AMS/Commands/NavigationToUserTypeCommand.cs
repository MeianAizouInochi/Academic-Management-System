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

            AwsDataAccessUserDetails awsDataAccessUserDetails = new AwsDataAccessUserDetails(new BasicUserDetails(), loginViewModel.Username, loginViewModel.Password);

            try
            {
                awsDataAccessUserDetails.GetData();

                InternalMenuNavigationStore internalMenuNavigationStore = new InternalMenuNavigationStore();

                internalMenuNavigationStore.CurrentSelectedFeatureViewModel = new UserDashboardViewModel((BasicUserDetails)awsDataAccessUserDetails.DataObject);

                navigationStore.CurrentViewModel = new StudentViewModel(internalMenuNavigationStore,(BasicUserDetails)awsDataAccessUserDetails.DataObject);
            }
            catch (AMSError_Exceptions ex) 
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}

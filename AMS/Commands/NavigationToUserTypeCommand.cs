using AMS.Store;
using AMS.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            if (loginViewModel.Username.Equals("student"))
            {
                Console.WriteLine("Good to go!");
                loginViewModel.LoggedIn = true;

                InternalMenuNavigationStore internalMenuNavigationStore = new InternalMenuNavigationStore();

                internalMenuNavigationStore.CurrentSelectedFeatureViewModel = new UserDashboardViewModel();

                navigationStore.CurrentViewModel = new StudentViewModel(internalMenuNavigationStore);
            }



        }
    }
}

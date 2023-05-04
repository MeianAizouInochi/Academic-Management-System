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
        }
    }
}

using AMS.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace AMS
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        /*
         * Function whose definition is getting changed for Startup of the application.
         * This is a virtual function inside Application class.
         */
        protected override void OnStartup(StartupEventArgs e)
        {
            /*
            * This is a Property inside Application class which is inherited by App class.
            * This property is getting initialized inside the overrided method OnStartup of Application class.
            * MainWindow Property is an object of Window Class.
            */
            MainWindow = new MainWindow()
            {
                DataContext = new AMSView_ViewModel()
            };

            MainWindow.Show();

            base.OnStartup(e);
        }
    }
}

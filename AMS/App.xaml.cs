using AMS.Store;
using AMS.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace AMS
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private SplashScreen splashScreen;
        /*
         * Function whose definition is getting changed for Startup of the application.
         * This is a virtual function inside Application class.
         */
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            /*
             * Imitating loading of data.
             */
            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += LoadData;
            worker.RunWorkerCompleted += ShowMainWindow;
            //worker.RunWorkerAsync();
            
            splashScreen = new SplashScreen();

            splashScreen.Loaded += (sender, args) => { worker.RunWorkerAsync(); };

            splashScreen.SplashScreen_Show();


        }

        private void ShowMainWindow(object? sender, RunWorkerCompletedEventArgs e)
        {
            //on placing the splashscreen close statement here the mainwindow never gets shown and the application closes.

            NavigationStore navigationStore = new NavigationStore();
            navigationStore.CurrentViewModel = new LoginViewModel(navigationStore);

            MainWindow  = new MainWindow()
            {
                DataContext = new MainViewModel(navigationStore)
            };

            splashScreen.SplashScreen_Close();

            MainWindow.Show();
        }

        private void LoadData(object? sender, DoWorkEventArgs e)
        {
            Thread.Sleep(13000); //imitating Loading of data(static data)
        }
    }
}

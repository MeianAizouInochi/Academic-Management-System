using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using AWS_MANAGEMENT_BRIDGE;
using DocumentFormat.OpenXml.Vml.Office;

namespace AMS.ViewModels
{
    public class AMSView_ViewModel
    {
        public DirectoryManager_ViewModel directoryManager_ViewModel { get; }

        public DocxModifier_ViewModel docxModifier_ViewModel { get; }

        public ICommand? ExitApplication { get; }

        public AMSView_ViewModel()
        {
            directoryManager_ViewModel = new DirectoryManager_ViewModel();

            docxModifier_ViewModel = new DocxModifier_ViewModel();

            //Debuging section start

            Entity e = new Entity();
            if (e.CreateS3Connection() == 1)
            {
                e.ListBucketS3();
                List<string> list = new List<string>(e.ListObjectsS3());
                //for (int j = 0; j < list.Count; j++)
                //{
                //    Console.WriteLine(list[j]);
                //}

                MessageBox.Show(list.Count().ToString());

            }
            else
            {
                MessageBox.Show("error occured");
                //Console.WriteLine("Error in creating S3 Connection!");
            }
            e.CloseConnection();

            //Debuging section end
        }
    }
}

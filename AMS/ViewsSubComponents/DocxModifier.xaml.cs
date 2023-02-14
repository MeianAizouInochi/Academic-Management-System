using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AMS.ViewsSubComponents
{
    /// <summary>
    /// Interaction logic for DocxModifier.xaml
    /// </summary>
    public partial class DocxModifier : UserControl
    {
        ///*------------------------------------------------------------Variables Start---------------------------------------------------------*/
        ///*
        // * The following Variables are subjected to change, when viewmodel and model of this component gets implemented.
        // */
        //private string? ChosenDocxFilePath;

        //private string? ChosenXlsxFilePath;

        //private string? ChosenPicFilePath;
        ///*------------------------------------------------------------Variables End---------------------------------------------------------*/

        //[DllImport("AwsManagementSystem.dll")]
        //public static extern IntPtr Connect(int param);

        //[DllImport("AwsManagementSystem.dll")]
        //public static extern int ExposeSomeFunction(IntPtr obj, int someparam);


        public DocxModifier()
        {
            InitializeComponent();
        }

        ///*
        //* The following Event Handlers are temporary implementation, untill viewmodel and model of this component gets implemented.
        //*/
        ///*------------------------------------------------------------Event Handlers Start---------------------------------------------------------*/

        ///*
        // * Handling OnCLick Event For the File Upload button.
        // */
        //private void UploadDocx_OnClick(object sender, RoutedEventArgs e)
        //{
        //    //Creating the Open file Window Object for opening the default file picker for Windows.
        //    Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();

        //    Nullable<bool> DidTheDialogOpen = openFileDialog.ShowDialog();

        //    if (DidTheDialogOpen == true)
        //    {
        //        ChosenDocxFilePath = openFileDialog.FileName;

        //        MessageBox.Show(ChosenDocxFilePath);
        //    }


        //}
        //private void UploadXlsx_OnClick(object sender, RoutedEventArgs e)
        //{
        //    //Creating the Open file Window Object for opening the default file picker for Windows.
        //    Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();

        //    Nullable<bool> DidTheDialogOpen = openFileDialog.ShowDialog();

        //    if (DidTheDialogOpen == true)
        //    {
        //        ChosenXlsxFilePath = openFileDialog.FileName;

        //        MessageBox.Show(ChosenXlsxFilePath);
        //    }


        //}

        //private void UploadPicture_OnClick(object sender, RoutedEventArgs e)
        //{
        //    Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();

        //    Nullable<bool> DidTheDialogOpen = openFileDialog.ShowDialog();

        //    if (DidTheDialogOpen == true)
        //    {
        //        ChosenPicFilePath = openFileDialog.FileName;
        //        MessageBox.Show(ChosenPicFilePath);
        //    }

        //}

        //private void ModifyFile_OnClick(object sender, RoutedEventArgs e)
        //{
        //    if (ChosenDocxFilePath != null && ChosenPicFilePath != null)
        //    {
        //        DocX document = DocX.Load(ChosenDocxFilePath);

        //        //string text = "WTF";
        //        var image = document.AddImage(ChosenPicFilePath);

        //        var picture = image.CreatePicture(96, 96); //in dpi

        //        // Do the replacement of all the found tags with the specified image and ignore the case when searching for the tags.
        //        document.ReplaceTextWithObject(new ObjectReplaceTextOptions { NewObject = picture, TrackChanges = false, SearchValue = "<SelfPicture>", RegExOptions = RegexOptions.IgnoreCase });

        //        //document.ReplaceText(new StringReplaceTextOptions { NewValue = "WTF",SearchValue="<THIS IS IT BOYS!!", RegExOptions = RegexOptions.IgnoreCase, StopAfterOneReplacement=true});

        //        document.SaveAs(ChosenDocxFilePath);


        //    }

        //}

        ///*
        // * Development Help Function for checking the dll.
        // */
        //private void DllConnectionCheck_OnClick(object sender, RoutedEventArgs e)
        //{
        //    IntPtr obj = Connect(10);

        //    MessageBox.Show(ExposeSomeFunction(obj, 20).ToString());
        //}
        ///*------------------------------------------------------------Event Handlers End---------------------------------------------------------*/

    }
}

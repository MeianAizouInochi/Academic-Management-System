using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

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
        }
    }
}

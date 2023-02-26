using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AMS.ViewModels
{
    public class DirectoryManager_ViewModel
    {
        private readonly ObservableCollection<DirectoryManager_DirectoryListItem_ViewModel>? _directoryManager_DirectoryListItem_ViewModels;

        public IEnumerable<DirectoryManager_DirectoryListItem_ViewModel>? DirectoryManager_DirectoryListItem_ViewModels => _directoryManager_DirectoryListItem_ViewModels;

        public ICommand? GotoParentDirectory { get; }

        public DirectoryManager_ViewModel(List<string> Folders)
        {
            ObservableCollection<DirectoryManager_DirectoryListItem_ViewModel>? Temp = new ObservableCollection<DirectoryManager_DirectoryListItem_ViewModel>();

            foreach (var folder in Folders) 
            {
                Temp.Add(new DirectoryManager_DirectoryListItem_ViewModel(folder));
            }

            _directoryManager_DirectoryListItem_ViewModels= Temp;

            //_directoryManager_DirectoryListItem_ViewModels = new ObservableCollection<DirectoryManager_DirectoryListItem_ViewModel>
            //{
            //    new DirectoryManager_DirectoryListItem_ViewModel("Folder1"),

            //    new DirectoryManager_DirectoryListItem_ViewModel("Folder2"),

            //    new DirectoryManager_DirectoryListItem_ViewModel("Folder3")
            //};

        }
    }
}

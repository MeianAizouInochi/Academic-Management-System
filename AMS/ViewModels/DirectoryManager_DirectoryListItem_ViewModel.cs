using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AMS.ViewModels
{
    public class DirectoryManager_DirectoryListItem_ViewModel:ViewModelMother
    {
        public string? File_or_Folder_Name { get; }

        public ICommand? MoveCommand { get; }

        public ICommand? DeleteCommand { get; }

        public DirectoryManager_DirectoryListItem_ViewModel(string file_or_Folder_Name)
        {
            File_or_Folder_Name = file_or_Folder_Name;
        }
    }
}

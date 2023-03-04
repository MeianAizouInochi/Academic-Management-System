using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMS.ViewModels
{
    public class DocxEditor_EditedListItem_ViewModel
    {
        string? Edited_File_Name;

        public DocxEditor_EditedListItem_ViewModel(string Edited_File_Name) 
        {
            this.Edited_File_Name = Edited_File_Name;
        }
    }
}

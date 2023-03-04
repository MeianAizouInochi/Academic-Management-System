using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMS.ViewModels
{
    public class DocxModifier_ViewModel
    {
        private readonly ObservableCollection<DocxEditor_EditedListItem_ViewModel>? _docxEditor_EditedListItem_ViewModels;

        public IEnumerable<DocxEditor_EditedListItem_ViewModel>? DocxEditor_EditedListItem_ViewModels => _docxEditor_EditedListItem_ViewModels;

        public string PathToXLSX { get; set; } = "Path of Xlsx...";
        
    }
}

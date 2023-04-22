using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMS.ViewModels
{
    //This is the interface for notifying the UI that some property has changed hence it needs to re-render.
    public class ViewModelBase : INotifyPropertyChanged //This helps to update the UI even if the property is changed inside the code by the code itself.
    {//Normal DataBinding without INotifyPropertyChanged Only updates UI when property is changed in the code from the UI interaction.
        public event PropertyChangedEventHandler? PropertyChanged;

        // This function helps to re-render. It needs to be called when a property which is the data context of UI is changed/ 
        //Or when ever due to change of some property the UI needs to re-render.
        protected void OnPropertyChanged(string? PropertyName = null) 
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
        }
    }
}

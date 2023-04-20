﻿using AMS.Commands;
using AMS.Store;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AMS.ViewModels
{
    public class StudentViewModel:ViewModelBase
    {
        public ICommand? NavigateToDashBoardCommand { get; }

        public ICommand? NavigateToTimeTableCommand { get; }

        public InternalMenuNavigationStore internalMenuNavigationStore;

        public ViewModelBase CurrentSelectedFeatureViewModel => internalMenuNavigationStore.CurrentSelectedFeatureViewModel;

        public StudentViewModel(InternalMenuNavigationStore internalmenuNavigationStore)
        {
            internalMenuNavigationStore = internalmenuNavigationStore;

            NavigateToDashBoardCommand = new NavigationToStudentDashboardCommand(internalmenuNavigationStore);

            NavigateToTimeTableCommand = new NavigationToStudentTimeTableCommand(internalmenuNavigationStore);

            internalMenuNavigationStore.CurrentSelectedFeatureViewModelChanged += OnCurrentSelectedFeatureViewModelChanged;

        }

        private void OnCurrentSelectedFeatureViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentSelectedFeatureViewModel));
        }
    }
}
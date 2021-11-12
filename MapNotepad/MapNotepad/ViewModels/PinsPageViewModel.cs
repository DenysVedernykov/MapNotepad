using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace MapNotepad.ViewModels
{
    class PinsPageViewModel : BaseViewModel
    {
        public PinsPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            
        }
    }
}

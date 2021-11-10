using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Text;

namespace MapNotepad.ViewModels
{
    class AddPinsPageViewModel : BaseViewModel
    {
        public AddPinsPageViewModel(INavigationService navigationService) 
            : base(navigationService)
        {

        }
    }
}

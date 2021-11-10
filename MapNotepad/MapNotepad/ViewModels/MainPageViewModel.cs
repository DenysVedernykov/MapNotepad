using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Text;

namespace MapNotepad.ViewModels
{
    class MainPageViewModel : BaseViewModel
    {
        public MainPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {

        }
    }
}

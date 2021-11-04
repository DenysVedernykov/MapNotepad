using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace MapNotepad.ViewModels
{
    class RegisterViewModel : BaseViewModel
    {
        public RegisterViewModel(INavigationService navigationService) : base(navigationService)
        {

        }

        public ICommand GoToBack => new Command((obj) =>
        {
            _navigationService.GoBackAsync();
        });
    }
}

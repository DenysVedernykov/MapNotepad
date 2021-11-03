using MapNotepad.Views;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace MapNotepad.ViewModels
{
    class StartViewModel : BaseViewModel
    {
        public StartViewModel(INavigationService navigationService) : base(navigationService)
        {
        }

        public ICommand ShowRegisterViewCommand => new Command((obj) =>
        {
            _navigationService.NavigateAsync(nameof(RegisterView));
        });

        public ICommand ShowLogInViewCommand => new Command((obj) =>
        {
            _navigationService.NavigateAsync(nameof(LogInView));
        });
    }
}

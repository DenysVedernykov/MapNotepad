using MapNotepad.Helpers;
using MapNotepad.Views;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace MapNotepad.ViewModels
{
    class StartPageViewModel : BaseViewModel
    {
        public StartPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
        }

        #region -- Public properties --

        private ICommand _showRegisterViewCommand;
        public ICommand ShowRegisterViewCommand => _showRegisterViewCommand ??= SingleExecutionCommand.FromFunc(OnShowRegisterCommandAsync);
        
        private ICommand _showLogInViewCommand;
        public ICommand ShowLogInViewCommand => _showLogInViewCommand ??= SingleExecutionCommand.FromFunc(OnShowLogInViewCommandAsync);

        #endregion

        #region -- Private methods --

        private Task OnShowLogInViewCommandAsync()
        {
            _navigationService.NavigateAsync(nameof(LogInPage));

            return Task.CompletedTask;
        }

        private Task OnShowRegisterCommandAsync()
        {
            _navigationService.NavigateAsync(nameof(RegisterPage));

            return Task.CompletedTask;
        }

        #endregion

    }
}

using MapNotepad.Helpers;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MapNotepad.ViewModels
{
    class LogInViewModel : BaseViewModel
    {
        public LogInViewModel(INavigationService navigationService) : base(navigationService)
        {

        }

        #region -- Public properties --

        private ICommand _goToBackCommand;
        public ICommand GoToBackCommand => _goToBackCommand ??= SingleExecutionCommand.FromFunc(OnGoToBackCommandAsync);
        private Task OnGoToBackCommandAsync()
        {
            _navigationService.GoBackAsync();

            return Task.CompletedTask;
        }

        #endregion
    }
}

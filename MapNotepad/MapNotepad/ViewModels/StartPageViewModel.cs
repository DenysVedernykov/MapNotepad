using MapNotepad.Helpers;
using MapNotepad.Views;
using Prism.Navigation;
using System.Threading.Tasks;
using System.Windows.Input;

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

        private async Task OnShowLogInViewCommandAsync()
        {
            await _navigationService.NavigateAsync(nameof(LogInPage));
        }

        private async Task OnShowRegisterCommandAsync()
        {
            await _navigationService.NavigateAsync(nameof(RegisterPage));
        }

        #endregion

    }
}

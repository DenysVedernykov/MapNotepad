using MapNotepad.Helpers;
using MapNotepad.Models;
using MapNotepad.Services.Authorization;
using MapNotepad.Services.Pins;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services.Dialogs;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace MapNotepad.ViewModels
{
    class ConfirmAddPinQrViewModel : BaseViewModel, IDialogAware
    {
        private IPinService _pinService;

        private IAuthorizationService _authorizationService;

        public ConfirmAddPinQrViewModel(
            INavigationService navigationService,
            IPinService pinService,
            IAuthorizationService authorizationService)
            : base(navigationService)
        {
            _pinService = pinService;
            _authorizationService = authorizationService;

            _canCloseDialog = true;
            CloseCommand = new DelegateCommand(() => { RequestClose(null); });
        }

        #region -- Public properties --

        private UserPin _pin;
        public UserPin Pin
        {
            get => _pin;
            set => SetProperty(ref _pin, value);
        }

        private bool _added;
        public bool Added
        {
            get => _added;
            set => SetProperty(ref _added, value);
        }

        private bool _canCloseDialog;
        public bool CanCloseDialog() => _canCloseDialog;

        public DelegateCommand CloseCommand { get; }

        public event Action<IDialogParameters> RequestClose;

        private ICommand _okCommand;
        public ICommand OkCommand => _okCommand ??= SingleExecutionCommand.FromFunc(OnOkCommandAsync);

        private ICommand _cancelCommand;
        public ICommand CancelCommand => _cancelCommand ??= SingleExecutionCommand.FromFunc(OnCancelCommandAsync);

        #endregion

        public void OnDialogClosed()
        {
            MessagingCenter.Send<ConfirmAddPinQrViewModel>(this, "StartScanning");
        }

        public void OnDialogOpened(IDialogParameters parameters)
        {
            if (parameters.Count > 0)
            {
                Pin = parameters["UserPin"] as UserPin;
            }
        }

        #region -- Private methods --

        private Task OnOkCommandAsync()
        {
            Added = true;

            Pin.Autor = _authorizationService.Profile.Id;
            Pin.CreationDate = DateTime.Now;

            var result = _pinService.AddPinAsync(Pin);

            if (result.Result.IsSuccess)
            {
                Pin.Id = result.Result.Result;

                MessagingCenter.Send<ConfirmAddPinQrViewModel, UserPin>(this, "AddPin", Pin);
            }

            return Task.CompletedTask;
        }

        private Task OnCancelCommandAsync()
        {
            RequestClose(null);

            return Task.CompletedTask;
        }

        #endregion
    }
}
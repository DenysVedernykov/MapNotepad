using MapNotepad.Helpers;
using MapNotepad.Models;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ZXing.Common;

namespace MapNotepad.ViewModels
{
    class ConfirmAddPinQrViewModel : BaseViewModel, IDialogAware
    {
        public ConfirmAddPinQrViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            _canCloseDialog = true;
            CloseCommand = new DelegateCommand(() => { RequestClose(null); });
        }

        #region -- Public properties --

        public DelegateCommand CloseCommand { get; }

        public event Action<IDialogParameters> RequestClose;

        private bool _canCloseDialog;
        public bool CanCloseDialog() => _canCloseDialog;

        private UserPin _pin;
        public UserPin Pin
        {
            get => _pin;
            set => SetProperty(ref _pin, value);
        }

        public void OnDialogClosed()
        {
        }

        public void OnDialogOpened(IDialogParameters parameters)
        {
            if (parameters.Count > 0)
            {
                Pin = parameters["UserPin"] as UserPin;
            }
        }

        #endregion

    }
}

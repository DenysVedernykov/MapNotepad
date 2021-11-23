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
    class QrCodePageViewModel : BaseViewModel, IDialogAware
    {
        private UserPin _pin;

        public QrCodePageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            _encodingOptions = new EncodingOptions()
            {
                Width = 500,
                Height = 500,
                Margin = 10
            };

            _canCloseDialog = true;
            CloseCommand = new DelegateCommand(() => { RequestClose(null); });
        }

        #region -- Public properties --

        private string _dataGrCode;
        public string DataGrCode
        {
            get => _dataGrCode;
            set => SetProperty(ref _dataGrCode, value);
        }

        private EncodingOptions _encodingOptions;
        public EncodingOptions EncodingOptions
        {
            get => _encodingOptions;
            set => SetProperty(ref _encodingOptions, value);
        }

        public DelegateCommand CloseCommand { get; }

        public event Action<IDialogParameters> RequestClose;

        private bool _canCloseDialog;
        public bool CanCloseDialog() => _canCloseDialog;

        public void OnDialogClosed()
        {
        }

        public void OnDialogOpened(IDialogParameters parameters)
        {
            if(parameters.Count > 0)
            {
                if(parameters["UserPin"] is not null)
                {
                    _pin = parameters["UserPin"] as UserPin;

                    DataGrCode = JsonConvert.SerializeObject(_pin);
                }
            }
        }

        #endregion

        #region -- Private methods --


        #endregion

    }
}

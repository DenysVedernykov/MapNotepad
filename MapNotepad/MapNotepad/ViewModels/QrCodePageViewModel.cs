using MapNotepad.Helpers;
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
        public QrCodePageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            _encodingOptions = new EncodingOptions()
            {
                Width = 300,
                Height = 300,
                Margin = 10
            };

            _canCloseDialog = true;
            CloseCommand = new DelegateCommand(() => { RequestClose(null); });
        }

        #region -- Public properties --

        private string _result;
        public string Result
        {
            get => _result;
            set => SetProperty(ref _result, value);
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
            
        }

        #endregion

        #region -- Private methods --


        #endregion

    }
}

using MapNotepad.Helpers;
using MapNotepad.Models;
using Newtonsoft.Json;
using Prism.Navigation;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using ZXing;

namespace MapNotepad.ViewModels
{
    public class ScanCodePageViewModel : BaseViewModel
    {
        private IDialogService _dialogService;

        public ScanCodePageViewModel(
            IDialogService dialogService, 
            INavigationService navigationService)
               : base(navigationService)
        {
            _dialogService = dialogService;

            MessagingCenter.Subscribe<ConfirmAddPinQrViewModel>(
                this,
                "StartScanning",
                (sender) => {
                    _shouldAnalyzing = true;
                    ShouldVisible = true;
                });
        }

        #region -- Public properties --

        private Result _result;
        public Result Result
        {
            get => _result;
            set => SetProperty(ref _result, value);
        }

        private bool _shouldVisible = true;
        public bool ShouldVisible
        {
            get => _shouldVisible;
            set => SetProperty(ref _shouldVisible, value);
        }

        private bool _shouldAnalyzing = true;

        private ICommand _scanResultCommand;
        public ICommand ScanResultCommand => _scanResultCommand ??= SingleExecutionCommand.FromFunc(OnScanResultCommandAsync);
        
        private ICommand _goBackCommand;
        public ICommand GoBackCommand => _goBackCommand ??= SingleExecutionCommand.FromFunc(OnGoBackCommandAsync);

        #endregion

        #region -- Private methods --

        private Task OnScanResultCommandAsync()
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                _shouldAnalyzing = false;

                try
                {
                    var recognized = JsonConvert.DeserializeObject<UserPin>(Result.Text);

                    if (recognized is UserPin && recognized is not null)
                    {
                        var param = new DialogParameters();
                        param.Add("UserPin", recognized);

                        ShouldVisible = false;

                        _dialogService.ShowDialog("ConfirmAddPinQr", param);
                    }
                    else
                    {
                        _shouldAnalyzing = true;
                    }
                }
                catch(Exception e)
                {
                    _shouldAnalyzing = true;
                }

            });

            return Task.CompletedTask;
        }

        private Task OnGoBackCommandAsync()
        {
            return _navigationService.GoBackAsync();
        }

        #endregion

    }
}

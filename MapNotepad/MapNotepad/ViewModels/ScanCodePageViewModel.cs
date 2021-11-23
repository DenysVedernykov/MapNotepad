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

        private bool _shouldScanning = true;
        public bool ShouldScanning
        {
            get => _shouldScanning;
            set => SetProperty(ref _shouldScanning, value);
        }

        private bool _shouldAnalyzing = true;
        public bool ShouldAnalyzing
        {
            get => _shouldAnalyzing;
            set => SetProperty(ref _shouldAnalyzing, value);
        }

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
                ShouldAnalyzing = false;
                ShouldScanning = false;

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
                    ShouldAnalyzing = true;
                    ShouldScanning = true;
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

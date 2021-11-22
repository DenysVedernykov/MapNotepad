﻿using MapNotepad.Helpers;
using MapNotepad.Views;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MapNotepad.ViewModels
{
    class SettingsPageViewModel : BaseViewModel
    {
        public SettingsPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {

        }

        #region -- Public properties --

        private ICommand _goScanCodeCommand;
        public ICommand GoScanCodeCommand => _goScanCodeCommand ??= SingleExecutionCommand.FromFunc(OnGoScanCodeCommandAsync);

        private ICommand _goBackCommand;
        public ICommand GoBackCommand => _goBackCommand ??= SingleExecutionCommand.FromFunc(OnGoBackCommandAsync);

        #endregion

        #region -- Private methods --

        private Task OnGoScanCodeCommandAsync()
        {
            return _navigationService.NavigateAsync(nameof(ScanCodePage));
        }

        private Task OnGoBackCommandAsync()
        {
            return _navigationService.GoBackAsync();
        }

        #endregion

    }
}

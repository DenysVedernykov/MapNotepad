﻿using MapNotepad.Helpers;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace MapNotepad.ViewModels
{
    class RegisterPageViewModel : BaseViewModel
    {
        public RegisterPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            
        }

        #region -- Public properties --

        private string _name;
        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        private string _email;
        public string Email
        {
            get => _email;
            set => SetProperty(ref _email, value);
        }

        private bool _isEnabledToolBarRightButton;
        public bool IsEnabledToolBarRightButton
        {
            get => _isEnabledToolBarRightButton;
            set => SetProperty(ref _isEnabledToolBarRightButton, value);
        }

        private ICommand _goBackCommand;
        public ICommand GoBackCommand => _goBackCommand ??= SingleExecutionCommand.FromFunc(OnGoBackCommandAsync);

        #endregion

        #region -- Overrides --

        protected override void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            base.OnPropertyChanged(args);

            switch (args.PropertyName)
            {
                case nameof(Name):
                case nameof(Email):
                    IsEnabledToolBarRightButton = !string.IsNullOrWhiteSpace(Name) && !string.IsNullOrWhiteSpace(Email);
                    break;
            }
        }

        #endregion

        #region -- Private methods --

        private Task OnGoBackCommandAsync()
        {
            _navigationService.GoBackAsync();

            return Task.CompletedTask;
        }

        #endregion

    }
}
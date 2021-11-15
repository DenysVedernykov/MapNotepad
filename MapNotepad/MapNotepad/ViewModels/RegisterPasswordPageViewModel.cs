using Acr.UserDialogs;
using MapNotepad.Helpers;
using MapNotepad.Models;
using MapNotepad.Services.Authorization;
using MapNotepad.Services.SettingsManager;
using MapNotepad.Views;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Unity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace MapNotepad.ViewModels
{
    class RegisterPasswordPageViewModel : BaseViewModel
    {

        #region -- Private properties --

        private User user;

        private bool _correctRepeatPassword;

        private bool _correctPassword;

        private ResourceDictionary _resourceDictionary;

        private IAuthorizationService _authorizationService;

        private ISettingsManagerService _settingsManagerService;

        #endregion

        public RegisterPasswordPageViewModel(
            INavigationService navigationService, 
            IAuthorizationService authorizationService,
            ISettingsManagerService settingsManagerService)
            : base(navigationService)
        {
            _authorizationService = authorizationService;
            _settingsManagerService = settingsManagerService;

            ICollection<ResourceDictionary> mergedDictionaries = PrismApplication.Current.Resources.MergedDictionaries;
            _resourceDictionary = mergedDictionaries.FirstOrDefault();

            IsEnabledCreateAccountButton = false;

            BorderColorPassword = (Color)_resourceDictionary["LightGray"];
            BorderColorRepeatPassword = (Color)_resourceDictionary["LightGray"];
        }

        #region -- Public properties --

        private string _password;
        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }

        private string _repeatPassword;
        public string RepeatPassword
        {
            get => _repeatPassword;
            set => SetProperty(ref _repeatPassword, value);
        }

        private string _errorMessagePassword;
        public string ErrorMessagePassword
        {
            get => _errorMessagePassword;
            set => SetProperty(ref _errorMessagePassword, value);
        }

        private string _errorMessageRepeatPassword;
        public string ErrorMessageRepeatPassword
        {
            get => _errorMessageRepeatPassword;
            set => SetProperty(ref _errorMessageRepeatPassword, value);
        }

        private Color _borderColorPassword;
        public Color BorderColorPassword
        {
            get => _borderColorPassword;
            set => SetProperty(ref _borderColorPassword, value);
        }

        private Color _borderColorRepeatPassword;
        public Color BorderColorRepeatPassword
        {
            get => _borderColorRepeatPassword;
            set => SetProperty(ref _borderColorRepeatPassword, value);
        }

        private bool _isEnabledCreateAccountButton;
        public bool IsEnabledCreateAccountButton
        {
            get => _isEnabledCreateAccountButton;
            set => SetProperty(ref _isEnabledCreateAccountButton, value);
        }

        private ICommand _goBackCommand;
        public ICommand GoBackCommand => _goBackCommand ??= SingleExecutionCommand.FromFunc(OnGoBackCommandAsync);
        
        private ICommand _createAccountCommand;
        public ICommand CreateAccountCommand => _createAccountCommand ??= SingleExecutionCommand.FromFunc(OnCreateAcccountCommandAsync);

        #endregion

        #region -- IInitializeAsync implementation --

        public async override Task InitializeAsync(INavigationParameters parameters)
        {
            if (parameters.Count > 0)
            {
                if (parameters["User"] != null)
                {
                    user = parameters["User"] as User;
                }
            }
        }

        #endregion

        #region -- Overrides --

        protected override void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            base.OnPropertyChanged(args);

            switch (args.PropertyName)
            {
                case nameof(Password):
                    _correctPassword = false;

                    if (string.IsNullOrWhiteSpace(Password))
                    {
                        BorderColorPassword = (Color)_resourceDictionary["Error"];
                        ErrorMessagePassword = Resource.ResourceManager.GetString("ErrorMessageEmptyPassword", Resource.Culture);
                    }
                    else
                    {
                        if (_authorizationService.PasswordMatching(Password))
                        {
                            _correctPassword = true;

                            BorderColorPassword = (Color)_resourceDictionary["LightGray"];
                            ErrorMessagePassword = string.Empty;
                        }
                        else
                        {
                            BorderColorPassword = (Color)_resourceDictionary["Error"];
                            ErrorMessagePassword = Resource.ResourceManager.GetString("PasswordIncorrect", Resource.Culture);
                        }
                    }

                    if (Password != RepeatPassword
                        && !string.IsNullOrWhiteSpace(Password)
                        && !string.IsNullOrWhiteSpace(RepeatPassword))
                    {
                        if (ErrorMessageRepeatPassword == string.Empty)
                        {
                            ErrorMessageRepeatPassword = Resource.ResourceManager.GetString("PasswordMismatch", Resource.Culture);
                        }
                    }

                    IsEnabledCreateAccountButton = 
                        _correctPassword 
                        && _correctRepeatPassword 
                        && (Password == RepeatPassword);
                    break;
                case nameof(RepeatPassword):
                    _correctRepeatPassword = false;

                    if (string.IsNullOrWhiteSpace(RepeatPassword))
                    {
                        BorderColorRepeatPassword = (Color)_resourceDictionary["Error"];
                        ErrorMessageRepeatPassword = Resource.ResourceManager.GetString("ErrorMessageEmptyPassword", Resource.Culture);
                    }
                    else
                    {
                        if (_authorizationService.PasswordMatching(RepeatPassword))
                        {
                            _correctRepeatPassword = true;

                            BorderColorRepeatPassword = (Color)_resourceDictionary["LightGray"];
                            ErrorMessageRepeatPassword = string.Empty;
                        }
                        else
                        {
                            BorderColorRepeatPassword = (Color)_resourceDictionary["Error"];
                            ErrorMessageRepeatPassword = Resource.ResourceManager.GetString("PasswordIncorrect", Resource.Culture);
                        }
                    }

                    if (Password != RepeatPassword
                        && !string.IsNullOrWhiteSpace(Password)
                        && !string.IsNullOrWhiteSpace(RepeatPassword))
                    {
                        if (ErrorMessageRepeatPassword == string.Empty)
                        {
                            ErrorMessageRepeatPassword = Resource.ResourceManager.GetString("PasswordMismatch", Resource.Culture);
                        }
                    }

                    IsEnabledCreateAccountButton =
                        _correctPassword
                        && _correctRepeatPassword
                        && (Password == RepeatPassword);
                    break;
            }
        }

        #endregion

        #region -- Private methods --

        private Task OnCreateAcccountCommandAsync()
        {
            user.Password = Password;

            if (_authorizationService.Registration(user))
            {
                _settingsManagerService.Email = user.Email;

                _navigationService.NavigateAsync(nameof(StartPage) + "/" + nameof(LogInPage));
            }
            else
            {
                UserDialogs.Instance.Alert(new AlertConfig()
                {
                    Message = Resource.ResourceManager.GetString("FailedRegister", Resource.Culture),
                    OkText = Resource.ResourceManager.GetString("Ok", Resource.Culture)
                });
            }

            return Task.CompletedTask;
        }

        private Task OnGoBackCommandAsync()
        {
            _navigationService.GoBackAsync();

            return Task.CompletedTask;
        }

        #endregion

    }
}

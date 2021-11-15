using Acr.UserDialogs;
using MapNotepad.Helpers;
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
    class LogInPageViewModel : BaseViewModel
    {
        private bool _correctEmail;
        private bool _correctPassword;

        private ResourceDictionary _resourceDictionary;

        private IAuthorizationService _authorizationService;

        private ISettingsManagerService _settingsManagerService;


        public LogInPageViewModel(
            INavigationService navigationService,
            IAuthorizationService authorizationService,
            ISettingsManagerService settingsManagerService)
            : base(navigationService)
        {
            _authorizationService = authorizationService;
            _settingsManagerService = settingsManagerService;

            ICollection<ResourceDictionary> mergedDictionaries = PrismApplication.Current.Resources.MergedDictionaries;
            _resourceDictionary = mergedDictionaries.FirstOrDefault();

            IsEnabledLogInButton = false;

            BorderColorEmail = (Color)_resourceDictionary["LightGray"];
            BorderColorPassword = (Color)_resourceDictionary["LightGray"];

            Email = _settingsManagerService.Email;
        }

        #region -- Public properties --

        private string _email;
        public string Email
        {
            get => _email;
            set => SetProperty(ref _email, value);
        }

        private string _password;
        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }

        private string _errorMessageEmail;
        public string ErrorMessageEmail
        {
            get => _errorMessageEmail;
            set => SetProperty(ref _errorMessageEmail, value);
        }

        private string _errorMessagePassword;
        public string ErrorMessagePassword
        {
            get => _errorMessagePassword;
            set => SetProperty(ref _errorMessagePassword, value);
        }

        private Color _borderColorEmail;
        public Color BorderColorEmail
        {
            get => _borderColorEmail;
            set => SetProperty(ref _borderColorEmail, value);
        }

        private Color _borderColorPassword;
        public Color BorderColorPassword
        {
            get => _borderColorPassword;
            set => SetProperty(ref _borderColorPassword, value);
        }

        private bool _isEnabledLogInButton;
        public bool IsEnabledLogInButton
        {
            get => _isEnabledLogInButton;
            set => SetProperty(ref _isEnabledLogInButton, value);
        }

        private ICommand _goBackCommand;
        public ICommand GoBackCommand => _goBackCommand ??= SingleExecutionCommand.FromFunc(OnGoBackCommandAsync);

        private ICommand _logInCommand;
        public ICommand LogInCommand => _logInCommand ??= SingleExecutionCommand.FromFunc(OnLogInCommandAsync);

        #endregion

        #region -- Overrides --

        protected override void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            base.OnPropertyChanged(args);

            switch (args.PropertyName)
            {
                case nameof(Email):
                    _correctEmail = false;

                    if (string.IsNullOrWhiteSpace(Email))
                    {
                        BorderColorEmail = (Color)_resourceDictionary["Error"];
                        ErrorMessageEmail = Resource.ResourceManager.GetString("ErrorMessageEmptyEmail", Resource.Culture);
                    }
                    else
                    {
                        if (_authorizationService.EmailMatching(Email))
                        {
                            if (!_authorizationService.CheckEmailForUse(Email))
                            {
                                _correctEmail = true;

                                BorderColorEmail = (Color)_resourceDictionary["LightGray"];
                                ErrorMessageEmail = string.Empty;
                            }
                            else
                            {
                                BorderColorEmail = (Color)_resourceDictionary["Error"];
                                ErrorMessageEmail = Resource.ResourceManager.GetString("EmailNotUsed", Resource.Culture);
                            }
                        }
                        else
                        {
                            BorderColorEmail = (Color)_resourceDictionary["Error"];
                            ErrorMessageEmail = Resource.ResourceManager.GetString("EmailIncorrect", Resource.Culture);
                        }
                    }

                    IsEnabledLogInButton = _correctEmail && _correctPassword;
                    break;
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

                    IsEnabledLogInButton = _correctEmail && _correctPassword;
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

        private Task OnLogInCommandAsync()
        {
            if (_authorizationService.Login(Email, Password))
            {
                _settingsManagerService.Session = "local";
                _settingsManagerService.Email = Email;
                _settingsManagerService.Password = Password;

                _navigationService.NavigateAsync("/" + nameof(MainPage));
            }
            else
            {
                _settingsManagerService.Email = string.Empty;

                Email = string.Empty;
                Password = string.Empty;

                UserDialogs.Instance.Alert(new AlertConfig()
                {
                    Message = Resource.ResourceManager.GetString("InvalidLoginOrPass", Resource.Culture),
                    OkText = Resource.ResourceManager.GetString("Ok", Resource.Culture)
                });
            }

            return Task.CompletedTask;
        }

        #endregion

    }
}

using MapNotepad.Helpers;
using MapNotepad.Models;
using MapNotepad.Services.Authorization;
using MapNotepad.Views;
using Prism.Navigation;
using Prism.Unity;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace MapNotepad.ViewModels
{
    class RegisterPageViewModel : BaseViewModel
    {
        private bool _correctName;
        private bool _correctEmail;

        private ResourceDictionary _resourceDictionary;

        IAuthorizationService _authorizationService;

        public RegisterPageViewModel(INavigationService navigationService, IAuthorizationService authorizationService)
            : base(navigationService)
        {
            _authorizationService = authorizationService;

            ICollection<ResourceDictionary> mergedDictionaries = PrismApplication.Current.Resources.MergedDictionaries;
            _resourceDictionary = mergedDictionaries.FirstOrDefault();

            IsEnabledNextButton = false;

            BorderColorName = (Color)_resourceDictionary["LightGray"];
            BorderColorEmail = (Color)_resourceDictionary["LightGray"];
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

        private string _errorMessageName;
        public string ErrorMessageName
        {
            get => _errorMessageName;
            set => SetProperty(ref _errorMessageName, value);
        }

        private string _errorMessageEmail;
        public string ErrorMessageEmail
        {
            get => _errorMessageEmail;
            set => SetProperty(ref _errorMessageEmail, value);
        }

        private Color _borderColorName;
        public Color BorderColorName
        {
            get => _borderColorName;
            set => SetProperty(ref _borderColorName, value);
        }

        private Color _borderColorEmail;
        public Color BorderColorEmail
        {
            get => _borderColorEmail;
            set => SetProperty(ref _borderColorEmail, value);
        }

        private bool _isEnabledNextButton;
        public bool IsEnabledNextButton
        {
            get => _isEnabledNextButton;
            set => SetProperty(ref _isEnabledNextButton, value);
        }

        private ICommand _goBackCommand;
        public ICommand GoBackCommand => _goBackCommand ??= SingleExecutionCommand.FromFunc(OnGoBackCommandAsync);
        

        private ICommand _goNextCommand;
        public ICommand GoNextCommand => _goNextCommand ??= SingleExecutionCommand.FromFunc(OnGoNextCommandAsync);

        #endregion

        #region -- Overrides --

        protected override void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            base.OnPropertyChanged(args);

            switch (args.PropertyName)
            {
                case nameof(Name):

                    _correctName = false;

                    if (string.IsNullOrWhiteSpace(Name))
                    {
                        BorderColorName = (Color)_resourceDictionary["Error"];
                        ErrorMessageName = Resource.ResourceManager.GetString("ErrorMessageEmptyName", Resource.Culture);
                    }
                    else
                    {
                        _correctName = true;

                        BorderColorName = (Color)_resourceDictionary["LightGray"];
                        ErrorMessageName = string.Empty;
                    }

                    IsEnabledNextButton = _correctName && _correctEmail;

                    break;
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
                            if (_authorizationService.CheckEmailForUse(Email))
                            {
                                _correctEmail = true;

                                BorderColorEmail = (Color)_resourceDictionary["LightGray"];
                                ErrorMessageEmail = string.Empty;
                            }
                            else
                            {
                                BorderColorEmail = (Color)_resourceDictionary["Error"];
                                ErrorMessageEmail = Resource.ResourceManager.GetString("EmailTaken", Resource.Culture);
                            }
                        }
                        else
                        {
                            BorderColorEmail = (Color)_resourceDictionary["Error"];
                            ErrorMessageEmail = Resource.ResourceManager.GetString("EmailIncorrect", Resource.Culture);
                        }
                    }

                    IsEnabledNextButton = _correctName && _correctEmail;

                    break;
            }
        }

        #endregion

        #region -- Private methods --

        private async Task OnGoNextCommandAsync()
        {
            User user = new User();
            user.Email = Email;
            user.Name = Name;

            NavigationParameters param = new NavigationParameters();
            param.Add("User", user);

            await _navigationService.NavigateAsync(nameof(RegisterPasswordPage), param);
        }

        private async Task OnGoBackCommandAsync()
        {
            await _navigationService.GoBackAsync();
        }

        #endregion

    }
}
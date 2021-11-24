using MapNotepad.Helpers;
using MapNotepad.Services.SettingsManager;
using MapNotepad.Themes;
using MapNotepad.Views;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Unity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace MapNotepad.ViewModels
{
    class SettingsPageViewModel : BaseViewModel
    {
        private ISettingsManagerService _settingsManagerService;

        public SettingsPageViewModel(
            INavigationService navigationService,
            ISettingsManagerService settingsManagerService)
            : base(navigationService)
        {
            _settingsManagerService = settingsManagerService;
            IsToggled = _settingsManagerService.NightTheme;
        }

        #region -- Public properties --

        private bool _isToggled;
        public bool IsToggled 
        { 
            get => _isToggled; 
            set => SetProperty(ref _isToggled, value); 
        }

        private ICommand _goScanCodeCommand;
        public ICommand GoScanCodeCommand => _goScanCodeCommand ??= SingleExecutionCommand.FromFunc(OnGoScanCodeCommandAsync);

        private ICommand _goBackCommand;
        public ICommand GoBackCommand => _goBackCommand ??= SingleExecutionCommand.FromFunc(OnGoBackCommandAsync);

        private ICommand _refreshCommand;
        public ICommand RefreshCommand => _refreshCommand ??= SingleExecutionCommand.FromFunc(OnRefreshCommandAsync);

        #endregion

        #region -- Overrides --

        protected override void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            base.OnPropertyChanged(args);

            switch (args.PropertyName)
            {
                case nameof(IsToggled):
                    _settingsManagerService.NightTheme = IsToggled;
                    break;
            }
        }

        #endregion

        #region -- Private methods --

        private Task OnRefreshCommandAsync()
        {
            ICollection<ResourceDictionary> mergedDictionaries = PrismApplication.Current.Resources.MergedDictionaries;
            if (mergedDictionaries != null)
            {
                mergedDictionaries.Clear();

                if (IsToggled)
                {
                    mergedDictionaries.Add(new DarkTheme());
                }
                else
                {
                    mergedDictionaries.Add(new LightTheme());
                }
            }

            return Task.CompletedTask;
        }

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

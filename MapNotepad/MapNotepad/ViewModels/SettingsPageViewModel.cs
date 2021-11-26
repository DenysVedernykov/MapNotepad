using MapNotepad.Helpers;
using MapNotepad.Services.PermissionsService;
using MapNotepad.Services.SettingsManager;
using MapNotepad.Themes;
using MapNotepad.Views;
using Prism.Navigation;
using Prism.Unity;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace MapNotepad.ViewModels
{
    class SettingsPageViewModel : BaseViewModel
    {
        private ISettingsManagerService _settingsManagerService;

        private IPermissionsService _permissionsService;

        public SettingsPageViewModel(
            INavigationService navigationService,
            ISettingsManagerService settingsManagerService,
            IPermissionsService permissionsService)
            : base(navigationService)
        {
            _settingsManagerService = settingsManagerService;
            _permissionsService = permissionsService;

            IsToggled = _settingsManagerService.IsNightThemeEnabled;
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
                    _settingsManagerService.IsNightThemeEnabled = IsToggled;
                    break;
            }
        }

        #endregion

        #region -- Private methods --

        private Task OnRefreshCommandAsync()
        {
            ICollection<ResourceDictionary> mergedDictionaries = PrismApplication.Current.Resources.MergedDictionaries;
            if (mergedDictionaries is not null)
            {
                mergedDictionaries.Clear();

                if (IsToggled)
                {
                    mergedDictionaries.Add(new DarkTheme());

                    MessagingCenter.Send<SettingsPageViewModel, string>(this, "ThemeChange", "MapNotepad.Themes.DarkMapTheme.txt");
                }
                else
                {
                    mergedDictionaries.Add(new LightTheme());

                    MessagingCenter.Send<SettingsPageViewModel, string>(this, "ThemeChange", "MapNotepad.Themes.LightMapTheme.txt");
                }
            }

            return Task.CompletedTask;
        }

        private async Task OnGoScanCodeCommandAsync()
        {
            var confirm = await _permissionsService.RequestAsync<Permissions.Camera>() == PermissionStatus.Granted;

            if (confirm)
            {
                await _navigationService.NavigateAsync(nameof(ScanCodePage));
            }
        }

        private async Task OnGoBackCommandAsync()
        {
            await _navigationService.GoBackAsync();
        }

        #endregion
    }
}

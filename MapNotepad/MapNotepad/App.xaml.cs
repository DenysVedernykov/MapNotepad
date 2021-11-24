using MapNotepad.Services.Authorization;
using MapNotepad.Services.GeolocationService;
using MapNotepad.Services.PermissionsService;
using MapNotepad.Services.Pins;
using MapNotepad.Services.Repository;
using MapNotepad.Services.Settings;
using MapNotepad.Services.SettingsManager;
using MapNotepad.Themes;
using MapNotepad.ViewModels;
using MapNotepad.Views;
using Prism;
using Prism.Ioc;
using Prism.Unity;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MapNotepad
{
    public partial class App : PrismApplication
    {
        public App()
        {
        }

        public App(IPlatformInitializer platformInitializer) : base(platformInitializer)
        {
        }

        #region --- Ovverides ---

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            //Services
            containerRegistry.RegisterInstance<IGeolocationService>(Container.Resolve<GeolocationService>());
            containerRegistry.RegisterInstance<IRepositoryService>(Container.Resolve<RepositoryService>());
            containerRegistry.RegisterInstance<IAuthorizationService>(Container.Resolve<AuthorizationService>());
            containerRegistry.RegisterInstance<ISettingsService>(Container.Resolve<SettingsService>());
            containerRegistry.RegisterInstance<ISettingsManagerService>(Container.Resolve<SettingsManagerService>());
            containerRegistry.RegisterInstance<IPinService>(Container.Resolve<PinService>());

            //Navigation
            containerRegistry.RegisterForNavigation<NavigationPage>();

            containerRegistry.RegisterForNavigation<ScanCodePage, ScanCodePageViewModel>();
            containerRegistry.RegisterForNavigation<MapPage, MapPageViewModel>();
            containerRegistry.RegisterForNavigation<StartPage, StartPageViewModel>();
            containerRegistry.RegisterForNavigation<LogInPage, LogInPageViewModel>();
            containerRegistry.RegisterForNavigation<RegisterPage, RegisterPageViewModel>();
            containerRegistry.RegisterForNavigation<RegisterPasswordPage, RegisterPasswordPageViewModel>();
            containerRegistry.RegisterForNavigation<MainPage, MainPageViewModel>();
            containerRegistry.RegisterForNavigation<PinsPage, PinsPageViewModel>();
            containerRegistry.RegisterForNavigation<AddPinsPage, AddPinsPageViewModel>();
            containerRegistry.RegisterForNavigation<EditPinsPage, EditPinsPageViewModel>();
            containerRegistry.RegisterForNavigation<SettingsPage, SettingsPageViewModel>();

            containerRegistry.RegisterDialog<QrCodePage, QrCodePageViewModel>();
            containerRegistry.RegisterDialog<ConfirmAddPinQr, ConfirmAddPinQrViewModel>();
        }

        protected override void OnInitialized()
        {
            InitializeComponent();

            Resource.Culture = new System.Globalization.CultureInfo("en-US");

            ICollection<ResourceDictionary> mergedDictionaries = Resources.MergedDictionaries;
            if (mergedDictionaries != null)
            {
                mergedDictionaries.Clear();

                if (Container.Resolve<SettingsManagerService>().NightTheme)
                {
                    mergedDictionaries.Add(new DarkTheme());
                }
                else
                {
                    mergedDictionaries.Add(new LightTheme());
                }
            }

            var authorization = Container.Resolve<IAuthorizationService>();
            var settingsManager = Container.Resolve<ISettingsManagerService>();

            var path = nameof(StartPage);

            if (settingsManager.Session == "local")
            {
                if (authorization.Login(settingsManager.Email, settingsManager.Password))
                {
                    path = nameof(MainPage);
                }
            }

            NavigationService.NavigateAsync(path);
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }

        #endregion

    }
}

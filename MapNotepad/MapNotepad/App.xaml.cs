using MapNotepad.Services.Authorization;
using MapNotepad.Services.Pins;
using MapNotepad.Services.Repository;
using MapNotepad.Services.Settings;
using MapNotepad.Services.SettingsManager;
using MapNotepad.Themes;
using MapNotepad.ViewModels;
using MapNotepad.Views;
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

        #region --- Ovverides ---

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            //Services
            containerRegistry.RegisterInstance<IRepositoryService>(Container.Resolve<RepositoryService>());
            containerRegistry.RegisterInstance<IAuthorizationService>(Container.Resolve<AuthorizationService>());
            containerRegistry.RegisterInstance<ISettingsService>(Container.Resolve<SettingsService>());
            containerRegistry.RegisterInstance<ISettingsManagerService>(Container.Resolve<SettingsManagerService>());
            containerRegistry.RegisterInstance<IPinsService>(Container.Resolve<PinsService>());

            //Navigation
            containerRegistry.RegisterForNavigation<NavigationPage>();

            containerRegistry.RegisterForNavigation<MapPage, MapPageViewModel>();
            containerRegistry.RegisterForNavigation<StartPage, StartPageViewModel>();
            containerRegistry.RegisterForNavigation<LogInPage, LogInPageViewModel>();
            containerRegistry.RegisterForNavigation<RegisterPage, RegisterPageViewModel>();
            containerRegistry.RegisterForNavigation<RegisterPasswordPage, RegisterPasswordPageViewModel>();
            containerRegistry.RegisterForNavigation<MainPage, MainPageViewModel>();
            containerRegistry.RegisterForNavigation<PinsPage, PinsPageViewModel>();
            containerRegistry.RegisterForNavigation<AddPinsPage, AddPinsPageViewModel>();
            containerRegistry.RegisterForNavigation<EditPinsPage, EditPinsPageViewModel>();
            containerRegistry.RegisterForNavigation<EventsPage, EventsPageViewModel>();
            containerRegistry.RegisterForNavigation<AddEventsPage, AddEventsPageViewModel>();
            containerRegistry.RegisterForNavigation<EditEventsPage, EditEventsPageViewModel>();
            containerRegistry.RegisterForNavigation<ShowPhotosPage, ShowPhotosPageViewModel>();
            containerRegistry.RegisterForNavigation<ShowClockPage, ShowClockPageViewModel>();
            containerRegistry.RegisterForNavigation<SettingsPage, SettingsPageViewModel>();
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

            if (settingsManager.Session == "local")
            {
                if (authorization.Login(settingsManager.Email, settingsManager.Password))
                {
                    NavigationService.NavigateAsync(nameof(MainPage));
                }
                else
                {
                    NavigationService.NavigateAsync(nameof(StartPage));
                }
            }
            else
            {
                NavigationService.NavigateAsync(nameof(StartPage));
            }
            
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

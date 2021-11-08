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
            containerRegistry.RegisterInstance<IRepository>(Container.Resolve<Repository>());
            containerRegistry.RegisterInstance<ISettings>(Container.Resolve<Settings>());
            containerRegistry.RegisterInstance<ISettingsManager>(Container.Resolve<SettingsManager>());

            //Navigation
            containerRegistry.RegisterForNavigation<NavigationPage>();

            containerRegistry.RegisterForNavigation<StartView, StartViewModel>();
            containerRegistry.RegisterForNavigation<LogInView, LogInViewModel>();
            containerRegistry.RegisterForNavigation<RegisterView, RegisterViewModel>();
            containerRegistry.RegisterForNavigation<RegisterPasswordView, RegisterPasswordViewModel>();
            containerRegistry.RegisterForNavigation<MainPageView, MainPageViewModel>();
            containerRegistry.RegisterForNavigation<PinsView, PinsViewModel>();
            containerRegistry.RegisterForNavigation<AddPinsView, AddPinsViewModel>();
            containerRegistry.RegisterForNavigation<EditPinsView, EditPinsViewModel>();
            containerRegistry.RegisterForNavigation<EventsView, EventsViewModel>();
            containerRegistry.RegisterForNavigation<AddEventsView, AddEventsViewModel>();
            containerRegistry.RegisterForNavigation<EditEventsView, EditEventsViewModel>();
            containerRegistry.RegisterForNavigation<ShowPhotosView, ShowPhotosViewModel>();
            containerRegistry.RegisterForNavigation<ShowClockView, ShowClockViewModel>();
            containerRegistry.RegisterForNavigation<SettingsView, SettingsViewModel>();
        }

        protected override void OnInitialized()
        {
            InitializeComponent();

            Resource.Culture = new System.Globalization.CultureInfo("en-US");

            ICollection<ResourceDictionary> mergedDictionaries = PrismApplication.Current.Resources.MergedDictionaries;
            if (mergedDictionaries != null)
            {
                mergedDictionaries.Clear();

                if (Container.Resolve<SettingsManager>().NightTheme)
                {
                    mergedDictionaries.Add(new DarkTheme());
                }
                else
                {
                    mergedDictionaries.Add(new LightTheme());
                }
            }

            NavigationService.NavigateAsync(nameof(StartView));
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

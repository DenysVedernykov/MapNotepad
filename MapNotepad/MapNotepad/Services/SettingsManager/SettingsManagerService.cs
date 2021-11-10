using System;
using System.Collections.Generic;
using System.Text;
using MapNotepad.Services.Settings;
using Xamarin.Essentials;

namespace MapNotepad.Services.SettingsManager
{
    public class SettingsManagerService : ISettingsManagerService
    {

        #region -- Private properties --

        private ISettingsService _settings;

        #endregion

        public SettingsManagerService(ISettingsService settings)
        {
            _settings = settings;
        }

        #region -- ISettingsManagerService implementation --

        public string Session
        {
            get => _settings.Session;
            set => _settings.Session = value;
        }

        public string Login
        {
            get => _settings.Login;
            set => _settings.Login = value;
        }

        public string Password
        {
            get => _settings.Password;
            set => _settings.Password = value;
        }
        public string ColorOfTheClock 
        {
            get => _settings.ColorOfTheClock;
            set => _settings.ColorOfTheClock = value;
        }

        public bool NightTheme
        {
            get => _settings.NightTheme;
            set => _settings.NightTheme = value;
        }

        #endregion

    }
}

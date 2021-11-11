using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;

namespace MapNotepad.Services.Settings
{
    public class SettingsService : ISettingsService
    {

        #region -- ISettingsService implementation --

        public string Session
        {
            get => Preferences.Get(nameof(Session), string.Empty);
            set => Preferences.Set(nameof(Session), value);
        }

        public string Email
        {
            get => Preferences.Get(nameof(Email), string.Empty);
            set => Preferences.Set(nameof(Email), value);
        }

        public string Password
        {
            get => Preferences.Get(nameof(Password), string.Empty);
            set => Preferences.Set(nameof(Password), value);
        }
        public string ColorOfTheClock
        {
            get => Preferences.Get(nameof(ColorOfTheClock), string.Empty);
            set => Preferences.Set(nameof(ColorOfTheClock), value);
        }

        public bool NightTheme
        {
            get => Preferences.Get(nameof(NightTheme), false);
            set => Preferences.Set(nameof(NightTheme), value);
        }

        #endregion

    }
}

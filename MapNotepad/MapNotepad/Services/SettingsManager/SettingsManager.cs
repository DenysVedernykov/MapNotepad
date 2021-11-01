using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;

namespace MapNotepad.Services.SettingsManager
{
    public class SettingsManager : ISettingsManager
    {
        public string Session
        {
            get => Preferences.Get(nameof(Session), "");
            set => Preferences.Set(nameof(Session), value);
        }
        public string Login
        {
            get => Preferences.Get(nameof(Login), "");
            set => Preferences.Set(nameof(Login), value);
        }
        public string Password
        {
            get => Preferences.Get(nameof(Password), "");
            set => Preferences.Set(nameof(Password), value);
        }
        public string ColorOfTheClock 
        {
            get => Preferences.Get(nameof(ColorOfTheClock), "");
            set => Preferences.Set(nameof(ColorOfTheClock), value);
        }
        public bool NightTheme
        {
            get => Preferences.Get(nameof(NightTheme), false);
            set => Preferences.Set(nameof(NightTheme), value);
        }
    }
}

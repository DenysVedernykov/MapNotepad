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

        public bool IsNightThemeEnabled
        {
            get => Preferences.Get(nameof(IsNightThemeEnabled), false);
            set => Preferences.Set(nameof(IsNightThemeEnabled), value);
        }

        #endregion

    }
}

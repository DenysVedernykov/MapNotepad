using MapNotepad.Services.Settings;

namespace MapNotepad.Services.SettingsManager
{
    public class SettingsManagerService : ISettingsManagerService
    {
        private ISettingsService _settings;

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

        public string Email
        {
            get => _settings.Email;
            set => _settings.Email = value;
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

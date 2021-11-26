namespace MapNotepad.Services.SettingsManager
{
    public interface ISettingsManagerService
    {
        string Session { get; set; }

        string Email { get; set; }

        string Password { get; set; }

        string ColorOfTheClock { get; set; }

        bool IsNightThemeEnabled { get; set; }
    }
}

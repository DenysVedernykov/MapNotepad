namespace MapNotepad.Services.Settings
{
    public interface ISettingsService
    {
        string Session { get; set; }

        string Email { get; set; }

        string Password { get; set; }

        string ColorOfTheClock { get; set; }

        bool IsNightThemeEnabled { get; set; }
    }
}

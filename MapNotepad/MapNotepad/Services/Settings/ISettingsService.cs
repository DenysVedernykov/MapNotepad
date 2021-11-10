using System;
using System.Collections.Generic;
using System.Text;

namespace MapNotepad.Services.Settings
{
    public interface ISettingsService
    {
        string Session { get; set; }

        string Login { get; set; }

        string Password { get; set; }

        string ColorOfTheClock { get; set; }

        bool NightTheme { get; set; }
    }
}

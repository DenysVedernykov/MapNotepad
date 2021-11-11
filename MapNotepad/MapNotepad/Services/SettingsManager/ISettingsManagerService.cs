using System;
using System.Collections.Generic;
using System.Text;

namespace MapNotepad.Services.SettingsManager
{
    public interface ISettingsManagerService
    {
        string Session { get; set; }

        string Email { get; set; }

        string Password { get; set; }

        string ColorOfTheClock { get; set; }

        bool NightTheme { get; set; }
    }
}

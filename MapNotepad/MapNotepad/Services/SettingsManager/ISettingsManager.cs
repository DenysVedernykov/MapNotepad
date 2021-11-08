﻿using System;
using System.Collections.Generic;
using System.Text;

namespace MapNotepad.Services.SettingsManager
{
    public interface ISettingsManager
    {
        string Session { get; set; }
        string Login { get; set; }
        string Password { get; set; }
        string ColorOfTheClock { get; set; }
        bool NightTheme { get; set; }
    }
}

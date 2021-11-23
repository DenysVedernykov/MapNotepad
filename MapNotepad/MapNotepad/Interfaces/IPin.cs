using System;
using System.Collections.Generic;
using System.Text;

namespace MapNotepad.Interfaces
{
    public interface IPin
    {
        string Label { get; set; }

        double Latitude { get; set; }

        double Longitude { get; set; }
    }
}

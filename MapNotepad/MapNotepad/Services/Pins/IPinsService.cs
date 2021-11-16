using MapNotepad.Helpers.ProcessHelpers;
using MapNotepad.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapNotepad.Services.Pins
{
    public interface IPinsService
    {
        Task<AOResult<int>> AddPin(UserPin pin);

        Task<AOResult<int>> UpdatePin(UserPin pin);

        Task<AOResult<int>> DeletePin(UserPin pin);

        Task<AOResult<IEnumerable>> AllPins();

        Task<AOResult<IEnumerable>> SearchPins(string text);
    }
}

using MapNotepad.Helpers.ProcessHelpers;
using MapNotepad.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MapNotepad.Services.Pins
{
    public interface IPinService
    {
        Task<AOResult<int>> AddPinAsync(UserPin pin);

        Task<AOResult<int>> UpdatePinAsync(UserPin pin);

        Task<AOResult<int>> DeletePinAsync(UserPin pin);

        Task<AOResult<List<UserPin>>> AllPinsAsync();

        Task<AOResult<UserPin>> GetByIdAsync(int id);

        Task<AOResult<List<UserPin>>> SearchPinsAsync(string text);
    }
}

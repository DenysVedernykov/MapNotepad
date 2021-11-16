using MapNotepad.Helpers.ProcessHelpers;
using MapNotepad.Models;
using MapNotepad.Services.Repository;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapNotepad.Services.Pins
{
    public class PinService : IPinService
    {
        private IRepositoryService _repositoryService;

        public PinService(IRepositoryService repositoryService)
        {
            _repositoryService = repositoryService;
        }

        public async Task<AOResult<int>> AddPinAsync(UserPin pin)
        {
            var result = new AOResult<int>();

            try
            {
                var id = await _repositoryService.InsertAsync(pin);

                result.SetSuccess(id);
            }
            catch (Exception ex)
            {
                result.SetError("0", "Exception PinsService AddPin", ex);
            }

            return result;
        }

        public async Task<AOResult<int>> UpdatePinAsync(UserPin pin)
        {
            var result = new AOResult<int>();

            try
            {
                var id = await  _repositoryService.UpdateAsync(pin);
                
                result.SetSuccess(id);
            }
            catch (Exception ex)
            {
                result.SetError("0", "Exception PinsService UpdatePin", ex);
            }

            return result;
        }

        public async Task<AOResult<int>> DeletePinAsync(UserPin pin)
        {
            var result = new AOResult<int>();

            try
            {
                var id = await _repositoryService.DeleteAsync(pin);
                
                result.SetSuccess(id);
            }
            catch (Exception ex)
            {
                result.SetError("0", "Exception PinsService DeletePin", ex);
            }

            return result;
        }

        public async Task<AOResult<List<UserPin>>> AllPinsAsync()
        {
            AOResult<List<UserPin>> result = new AOResult<List<UserPin>>();

            try
            {
                var response = await _repositoryService.GetAllRowsAsync<UserPin>();
                
                result.SetSuccess(response);
            }
            catch (Exception ex)
            {
                result.SetError("0", "Exception PinsService AllPins", ex);
            }

            return result;
        }

        public async Task<AOResult<List<UserPin>>> SearchPinsAsync(string text)
        {
            text = text.ToLower();

            var result = new AOResult<List<UserPin>>();

            try
            {
                var response = await _repositoryService.GetAllRowsAsync<UserPin>();
                
                result.SetSuccess(response.Where(
                        row =>
                        row.Label.ToLower().IndexOf(text) != -1
                        | row.Description.ToLower().IndexOf(text) != -1
                        | row.Longitude.ToString().ToLower().IndexOf(text) != -1
                        | row.Latitude.ToString().ToLower().IndexOf(text) != -1
                    ).ToList()
                );
            }
            catch (Exception ex)
            {
                result.SetError("0", "Exception PinsService AllPins", ex);
            }

            return result;
        }
    }
}

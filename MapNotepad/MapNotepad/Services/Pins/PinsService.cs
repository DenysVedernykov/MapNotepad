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
    public class PinsService : IPinsService
    {
        private IRepositoryService _repositoryService;

        public PinsService(IRepositoryService repositoryService)
        {
            _repositoryService = repositoryService;
        }

        public async Task<AOResult<int>> AddPin(UserPin pin)
        {
            AOResult<int> result = new AOResult<int>();

            try
            {
                if (pin == null)
                {
                    result.SetFailure();
                }
                else
                {
                    var response = _repositoryService.InsertAsync(pin);
                    if (response == null)
                    {
                        result.SetFailure();
                    }
                    else
                    {
                        result.SetSuccess(response.Result);
                    }
                }
            }
            catch (Exception ex)
            {
                result.SetError("0", "Exception PinsService AddPin", ex);
            }

            return result;
        }

        public Task<AOResult<int>> UpdatePin(UserPin pin)
        {
            Task<AOResult<int>> result = null;

            try
            {
                if (pin == null)
                {
                    result.Result.SetFailure();
                }
                else
                {
                    var response = _repositoryService.UpdateAsync(pin);
                    if (response == null)
                    {
                        result.Result.SetFailure();
                    }
                    else
                    {
                        result.Result.SetSuccess(response.Result);
                    }
                }
            }
            catch (Exception ex)
            {
                result.Result.SetError("0", "Exception PinsService UpdatePin", ex);
            }

            return result;
        }

        public Task<AOResult<int>> DeletePin(UserPin pin)
        {
            Task<AOResult<int>> result = null;

            try
            {
                if (pin == null)
                {
                    result.Result.SetFailure();
                }
                else
                {
                    var response = _repositoryService.DeleteAsync(pin);
                    if (response == null)
                    {
                        result.Result.SetFailure();
                    }
                    else
                    {
                        result.Result.SetSuccess(response.Result);
                    }
                }
            }
            catch (Exception ex)
            {
                result.Result.SetError("0", "Exception PinsService DeletePin", ex);
            }

            return result;
        }

        public Task<AOResult<IEnumerable>> AllPins()
        {
            Task<AOResult<IEnumerable>> result = null;

            try
            {
                var response = _repositoryService.GetAllRowsAsync<UserPin>();
                if (response == null)
                {
                    result.Result.SetFailure();
                }
                else
                {
                    result.Result.SetSuccess(response.Result);
                }
            }
            catch (Exception ex)
            {
                result.Result.SetError("0", "Exception PinsService AllPins", ex);
            }

            return result;
        }

        public Task<AOResult<IEnumerable>> SearchPins(string text)
        {
            Task<AOResult<IEnumerable>> result = null;

            try
            {
                var response = _repositoryService.GetAllRowsAsync<UserPin>();
                if (response == null)
                {
                    result.Result.SetFailure();
                }
                else
                {
                    result.Result.SetSuccess(
                        response.Result.Where(
                            row => 
                            row.Label == text 
                            | row.Description == text
                            | row.Longitude.ToString() == text
                            | row.Latitude.ToString() == text
                        )
                    );
                }
            }
            catch (Exception ex)
            {
                result.Result.SetError("0", "Exception PinsService AllPins", ex);
            }

            return result;
        }
    }
}

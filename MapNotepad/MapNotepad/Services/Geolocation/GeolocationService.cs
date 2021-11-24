using MapNotepad.Helpers.ProcessHelpers;
using MapNotepad.Services.GeolocationService;
using Plugin.Geolocator.Abstractions;
using System;
using Xamarin.Essentials;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace MapNotepad.Services.GeolocationService
{
    public class GeolocationService : IGeolocationService
    {
        private CancellationTokenSource cts;

        public async Task<AOResult<Location>> GetLastKnownLocation()
        {
            var result = new AOResult<Location>();

            try
            {
                var location = await Geolocation.GetLastKnownLocationAsync();

                if (location is not null)
                {
                    result.SetSuccess(location);
                }
                else
                {
                    result.SetFailure();
                }
            }
            catch (Exception ex)
            {
                result.SetError("0", "Exception GeolocationService GetLastKnownLocation", ex);
            }

            return result;
        }

        public async Task<AOResult<Location>> GetLocation()
        {
            var result = new AOResult<Location>();

            try
            {
                var request = new GeolocationRequest(GeolocationAccuracy.Medium, TimeSpan.FromSeconds(10));
                cts = new CancellationTokenSource();

                var location = await Geolocation.GetLocationAsync(request, cts.Token);

                if (location is not null)
                {
                    result.SetSuccess(location);
                }
                else
                {
                    result.SetFailure();
                }
            }
            catch (Exception ex)
            {
                result.SetError("0", "Exception GeolocationService GetLocation", ex);
            }

            return result;
        }

        public async Task<AOResult<Location>> GetCurrentPosition()
        {
            var result = new AOResult<Location>();

            try
            {
                var location = await GetLocation();

                if (!location.IsSuccess)
                {

                    location = await GetLastKnownLocation();

                    if (!location.IsSuccess)
                    {
                        result.SetFailure();
                    }
                    else
                    {
                        result.SetSuccess(location.Result);
                    }
                }
                else
                {
                    result.SetSuccess(location.Result);
                }
            }
            catch (Exception ex)
            {
                result.SetError("0", "Exception GeolocationService GetCurrentPosition", ex);
            }

            return result;
        }
    }
}

using MapNotepad.Helpers.ProcessHelpers;
using Plugin.Geolocator.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace MapNotepad.Services.GeolocationService
{
    public interface IGeolocationService
    {
        Task<AOResult<Location>> GetLocation();

        Task<AOResult<Location>> GetLastKnownLocation();

        Task<AOResult<Location>> GetCurrentPosition();
    }
}

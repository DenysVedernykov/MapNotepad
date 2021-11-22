using MapNotepad.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MapNotepad.Helpers
{
    public static class UserPinExtension
    {
        public static UserPin ToUserPin(this UserPinWithCommand pin)
        {
            return new UserPin()
            {
                Id = pin.Id,
                Address = pin.Address,
                Autor = pin.Autor,
                CreationDate = pin.CreationDate,
                Description = pin.Description,
                Favorites = pin.Favorites,
                Label = pin.Label,
                Latitude = pin.Latitude,
                Longitude = pin.Longitude
            };
        }

        public static UserPinWithCommand ToUserPinWithCommand(this UserPin pin)
        {
            return new UserPinWithCommand()
            {
                Id = pin.Id,
                Address = pin.Address,
                Autor = pin.Autor,
                CreationDate = pin.CreationDate,
                Description = pin.Description,
                Favorites = pin.Favorites,
                Label = pin.Label,
                Latitude = pin.Latitude,
                Longitude = pin.Longitude
            };
        }
    }
}

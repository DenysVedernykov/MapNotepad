using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace MapNotepad.Models
{
    public class UserPinWithCommand: UserPin
    {
        public UserPinWithCommand(UserPin pin)
        {
            Id = pin.Id;
            Autor = pin.Autor;
            Label = pin.Label;
            Address = pin.Address;
            Description = pin.Description;
            Latitude = pin.Latitude;
            Longitude = pin.Longitude;
            Favorites = pin.Favorites;
            CreationDate = pin.CreationDate;
        }

        public ICommand TabCommand { get; set; }

        public ICommand DeleteCommand { get; set; }

        public ICommand EditCommand { get; set; }
    }
}

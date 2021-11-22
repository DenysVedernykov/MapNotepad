using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace MapNotepad.Models
{
    public class UserPinWithCommand : BindableBase
    {
        private int _id;
        public int Id
        {
            get => _id;
            set => SetProperty(ref _id, value);
        }

        private int _autor;
        public int Autor
        {
            get => _autor;
            set => SetProperty(ref _autor, value);
        }

        private string _label;
        public string Label
        {
            get => _label;
            set => SetProperty(ref _label, value);
        }

        private string _address;
        public string Address
        {
            get => _address;
            set => SetProperty(ref _address, value);
        }

        private string _description;
        public string Description
        {
            get => _description;
            set => SetProperty(ref _description, value);
        }

        private double _latitude;
        public double Latitude
        {
            get => _latitude;
            set => SetProperty(ref _latitude, value);
        }

        private double _longitude;
        public double Longitude
        {
            get => _longitude;
            set => SetProperty(ref _longitude, value);
        }

        private bool _favorites;
        public bool Favorites
        {
            get => _favorites;
            set => SetProperty(ref _favorites, value);
        }

        private DateTime _creationDate;
        public DateTime CreationDate
        {
            get => _creationDate;
            set => SetProperty(ref _creationDate, value);
        }

        private ICommand _tabCommand;
        public ICommand TabCommand 
        {
            get => _tabCommand;
            set => SetProperty(ref _tabCommand, value);
        }

        private ICommand _deleteCommand;
        public ICommand DeleteCommand
        {
            get => _deleteCommand;
            set => SetProperty(ref _deleteCommand, value);
        }

        private ICommand _editCommand;
        public ICommand EditCommand
        {
            get => _editCommand;
            set => SetProperty(ref _editCommand, value);
        }

        private ICommand _switchFavoritesCommand;
        public ICommand SwitchFavoritesCommand
        {
            get => _switchFavoritesCommand;
            set => SetProperty(ref _switchFavoritesCommand, value);
        }
    }
}

using MapNotepad.Models;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace MapNotepad.ViewModels
{
    class PinsPageViewModel : BaseViewModel
    {
        public PinsPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            IsEmpty = false;

            All = new ObservableCollection<UserPin>
            {
                new UserPin
                {
                    Label = "Chimpanzee 1",
                    Description = "Hominidae 1",
                    Favorites = true,
                    Latitude = 46.5445454f,
                    Longitude = 55.7411236f
                },
                new UserPin
                {
                    Label = "Chimpanzee 2",
                    Description = "Hominidae 2",
                    Favorites = false,
                    Latitude = 46.5445454f,
                    Longitude = 55.7411236f
                },
                new UserPin
                {
                    Label = "Chimpanzee 3",
                    Description = "Hominidae 3",
                    Favorites = false,
                    Latitude = 46.5445454f,
                    Longitude = 55.7411236f
                },
                new UserPin
                {
                    Label = "Chimpanzee 4",
                    Description = "Hominidae 4",
                    Favorites = true,
                    Latitude = 46.5445454f,
                    Longitude = 55.7411236f
                },
                new UserPin
                {
                    Label = "Chimpanzee 5",
                    Description = "Hominidae 5",
                    Favorites = true,
                    Latitude = 46.5445454f,
                    Longitude = 55.7411236f
                },
                new UserPin
                {
                    Label = "Chimpanzee 6",
                    Description = "Hominidae 6",
                    Favorites = true,
                    Latitude = 46.5445454f,
                    Longitude = 55.7411236f
                },
                new UserPin
                {
                    Label = "Chimpanzee 7",
                    Description = "Hominidae 7",
                    Favorites = false,
                    Latitude = 46.5445454f,
                    Longitude = 55.7411236f
                },
                new UserPin
                {
                    Label = "Chimpanzee 8",
                    Description = "Hominidae 8",
                    Favorites = false,
                    Latitude = 46.5445454f,
                    Longitude = 55.7411236f
                },
                new UserPin
                {
                    Label = "Chimpanzee 9",
                    Description = "Hominidae 9",
                    Favorites = true,
                    Latitude = 46.5445454f,
                    Longitude = 55.7411236f
                },
                new UserPin
                {
                    Label = "Chimpanzee 10",
                    Description = "Hominidae 10",
                    Favorites = true,
                    Latitude = 46.5445454f,
                    Longitude = 55.7411236f
                }
            };
        }

        #region -- Public properties --

        private bool _isEmpty;
        public bool IsEmpty
        {
            get => _isEmpty;
            set => SetProperty(ref _isEmpty, value);
        }

        private string _text;
        public string Text
        {
            get => _text;
            set => SetProperty(ref _text, value);
        }

        public ObservableCollection<UserPin> All { get; set; }

        #endregion
    }
}

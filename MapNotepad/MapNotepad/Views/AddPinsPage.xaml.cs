﻿using MapNotepad.ViewModels;
using Plugin.Geolocator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;
using Xamarin.Forms.Xaml;

namespace MapNotepad.Views
{
    public partial class AddPinsPage : BaseContentPage
    {
        public AddPinsPage()
        {
            MessagingCenter.Subscribe<AddPinsPageViewModel, Pin>(
                this,
                "DeletePin",
                (sender, pin) => {
                    map.Pins.Remove(pin);
                });

            MessagingCenter.Subscribe<AddPinsPageViewModel, Pin>(
                this,
                "AddPin",
                (sender, pin) => {
                    map.Pins.Add(pin);
                });

            MessagingCenter.Subscribe<AddPinsPageViewModel, Position>(
                this,
                "MoveToLocation",
                async (sender, position) => {
                    await map.MoveCamera(CameraUpdateFactory.NewPosition(position));
                });

            InitializeComponent();
        }
    }
}
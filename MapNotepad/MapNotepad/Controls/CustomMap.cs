using MapNotepad.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;

namespace MapNotepad.Controls
{
    public class CustomMap : Map
    {
        public CustomMap()
        {
            UiSettings.MyLocationButtonEnabled = true;
            MyLocationEnabled = true;
            UiSettings.ZoomControlsEnabled = true;
        }

        #region --- Public Properties --- 

        public static readonly BindableProperty TextProperty = BindableProperty.Create(
            propertyName: nameof(Text),
            returnType: typeof(string),
            declaringType: typeof(CustomMap),
            defaultValue: string.Empty,
            defaultBindingMode: BindingMode.TwoWay);

        public string Text
        {
            set => SetValue(TextProperty, value);
            get => (string)GetValue(TextProperty);
        }

        public static readonly BindableProperty PinsSourceProperty = BindableProperty.Create(
            propertyName: nameof(PinsSource),
            returnType: typeof(ObservableCollection<Pin>),
            declaringType: typeof(CustomMap),
            defaultValue: null,
            defaultBindingMode: BindingMode.TwoWay);

        public ObservableCollection<Pin> PinsSource
        {
            get => (ObservableCollection<Pin>)GetValue(PinsSourceProperty);
            set => SetValue(PinsSourceProperty, value);
        }

        #endregion

        protected override void OnPropertyChanged(string propertyName)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == nameof(PinsSource) && PinsSource != null)
            {
                UpdatePins();
            }
        }

        #region -- Private helpers -- 

        private void UpdatePins()
        {
            Pins.Clear();

            foreach (var pin in PinsSource)
            {
                Pins.Add(pin);

            }
        }

        #endregion
    }
}
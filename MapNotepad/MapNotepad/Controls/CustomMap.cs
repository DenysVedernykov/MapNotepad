using MapNotepad.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;

namespace MapNotepad.Controls
{
    public class CustomMap : Map, IEnumerable<Pin>
    {
        public CustomMap()
        {
            UiSettings.MyLocationButtonEnabled = false;
            UiSettings.ZoomControlsEnabled = false;
        }

        #region -- Public properties --

        public static readonly BindableProperty ItemsSourceProperty = BindableProperty.Create(
            nameof(IEnumerable),
            typeof(IEnumerable), 
            typeof(CustomMap), 
            default(IEnumerable),
            propertyChanged: (b, o, n) => 
            ((CustomMap)b).OnItemsSourcePropertyChanged((IEnumerable)o, (IEnumerable)n));

        public IEnumerable ItemsSource
        {
            get => (IEnumerable)GetValue(ItemsSourceProperty);
            set => SetValue(ItemsSourceProperty, value);
        }

        #endregion

        #region -- Private methods --

        private void OnItemsSourcePropertyChanged(IEnumerable oldItemsSource, IEnumerable newItemsSource)
        {
            if (oldItemsSource is INotifyCollectionChanged ncc)
            {
                ncc.CollectionChanged -= OnItemsSourceCollectionChanged;
            }

            if (newItemsSource is INotifyCollectionChanged ncc1)
            {
                ncc1.CollectionChanged += OnItemsSourceCollectionChanged;
            }

            Pins.Clear();
            CreatePinItems(ItemsSource);
        }

        private void OnItemsSourceCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    if (e.NewStartingIndex == -1)
                        goto case NotifyCollectionChangedAction.Reset;

                    CreatePinItems(e.NewItems);
                    break;
                case NotifyCollectionChangedAction.Move:
                    if (e.OldStartingIndex == -1 || e.NewStartingIndex == -1)
                        goto case NotifyCollectionChangedAction.Reset;

                    break;
                case NotifyCollectionChangedAction.Remove:
                    if (e.OldStartingIndex == -1)
                        goto case NotifyCollectionChangedAction.Reset;

                    RemovePinItems(e.OldItems);
                    break;
                case NotifyCollectionChangedAction.Replace:
                    if (e.OldStartingIndex == -1)
                        goto case NotifyCollectionChangedAction.Reset;

                    RemovePinItems(e.OldItems);
                    CreatePinItems(e.NewItems);
                    break;
                case NotifyCollectionChangedAction.Reset:
                    Pins.Clear();
                    break;
            }
        }

        private void RemovePinItems(IEnumerable items)
        {
            foreach (object item in items)
            {
                var pin = item as Pin;
                Pins.Add(pin);
            }
        }

        private void CreatePinItems(IEnumerable items)
        {
            foreach (object item in items)
            {
                var pin = item as Pin;
                Pins.Add(pin);
            }
        }

        #endregion
    }
}
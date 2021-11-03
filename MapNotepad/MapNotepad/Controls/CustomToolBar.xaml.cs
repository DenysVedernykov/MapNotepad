using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MapNotepad.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CustomToolBar : ContentView
    {
        public static readonly BindableProperty TitleProperty =
            BindableProperty.Create("Title", typeof(string), typeof(CustomToolBar), string.Empty, BindingMode.TwoWay);
        public string Title
        {
            set
            {
                SetValue(TitleProperty, value);
            }
            get
            {
                return (string)GetValue(TitleProperty);
            }
        }

        public static readonly BindableProperty TitleColorProperty =
            BindableProperty.Create("TitleColor", typeof(Color), typeof(CustomToolBar), Color.Black, BindingMode.TwoWay);
        public Color TitleColor
        {
            set
            {
                SetValue(TitleColorProperty, value);
            }
            get
            {
                return (Color)GetValue(TitleColorProperty);
            }
        }

        public static readonly BindableProperty BackColorProperty =
            BindableProperty.Create("BackColor", typeof(Color), typeof(CustomToolBar), Color.White, BindingMode.TwoWay);
        public Color BackColor
        {
            set
            {
                SetValue(BackColorProperty, value);
            }
            get
            {
                return (Color)GetValue(BackColorProperty);
            }
        }

        public CustomToolBar()
        {
            InitializeComponent();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MapNotepad.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CustomEntry : ContentView
    {
        public static readonly BindableProperty ButtonCommandProperty =
            BindableProperty.Create("ButtonCommand", typeof(ICommand), typeof(CustomToolBar), null, BindingMode.TwoWay);
        public ICommand ButtonCommand
        {
            set
            {
                SetValue(ButtonCommandProperty, value);
            }
            get
            {
                return (ICommand)GetValue(ButtonCommandProperty);
            }
        }

        public static readonly BindableProperty IsVisibleButtonProperty =
            BindableProperty.Create("IsVisibleButton", typeof(bool), typeof(CustomToolBar), true, BindingMode.TwoWay);
        public bool IsVisibleButton
        {
            set
            {
                SetValue(IsVisibleButtonProperty, value);
            }
            get
            {
                return (bool)GetValue(IsVisibleButtonProperty);
            }
        }

        public static readonly BindableProperty PlaceholderProperty =
            BindableProperty.Create("Placeholder", typeof(string), typeof(CustomToolBar), string.Empty, BindingMode.TwoWay);
        public string Placeholder
        {
            set
            {
                SetValue(PlaceholderProperty, value);
            }
            get
            {
                return (string)GetValue(PlaceholderProperty);
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

        public static readonly BindableProperty BorderColorProperty =
            BindableProperty.Create("BorderColor", typeof(Color), typeof(CustomToolBar), Color.White, BindingMode.TwoWay);
        public Color BorderColor
        {
            set
            {
                SetValue(BorderColorProperty, value);
            }
            get
            {
                return (Color)GetValue(BorderColorProperty);
            }
        }

        public static readonly BindableProperty SourceProperty =
            BindableProperty.Create("Source", typeof(string), typeof(CustomToolBar), string.Empty, BindingMode.TwoWay);
        public string Source
        {
            set
            {
                SetValue(SourceProperty, value);
            }
            get
            {
                return (string)GetValue(SourceProperty);
            }
        }

        public static readonly BindableProperty TextProperty =
            BindableProperty.Create("Text", typeof(string), typeof(CustomToolBar), string.Empty, BindingMode.TwoWay);
        public string Text
        {
            set
            {
                SetValue(TextProperty, value);
            }
            get
            {
                return (string)GetValue(TextProperty);
            }
        }

        public CustomEntry()
        {
            InitializeComponent();
        }
    }
}
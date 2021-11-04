using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MapNotepad.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CustomToolBar : ContentView
    {
        public static readonly BindableProperty LeftButtonCommandProperty =
            BindableProperty.Create("LeftButtonCommand", typeof(ICommand), typeof(CustomToolBar), null, BindingMode.TwoWay);
        public ICommand LeftButtonCommand
        {
            set
            {
                SetValue(LeftButtonCommandProperty, value);
            }
            get
            {
                return (ICommand)GetValue(LeftButtonCommandProperty);
            }
        }

        public static readonly BindableProperty RigthButtonCommandProperty =
            BindableProperty.Create("RigthButtonCommand", typeof(ICommand), typeof(CustomToolBar), null, BindingMode.TwoWay);
        public ICommand RigthButtonCommand
        {
            set
            {
                SetValue(RigthButtonCommandProperty, value);
            }
            get
            {
                return (ICommand)GetValue(RigthButtonCommandProperty);
            }
        }

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

        public static readonly BindableProperty FontFamilyProperty =
            BindableProperty.Create("FontFamily", typeof(string), typeof(CustomToolBar), string.Empty, BindingMode.TwoWay);
        public string FontFamily
        {
            set
            {
                SetValue(FontFamilyProperty, value);
            }
            get
            {
                return (string)GetValue(FontFamilyProperty);
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

        public static readonly BindableProperty SourceLeftImageProperty =
            BindableProperty.Create("SourceLeftImage", typeof(string), typeof(CustomToolBar), string.Empty, BindingMode.TwoWay);
        public string SourceLeftImage
        {
            set
            {
                SetValue(SourceLeftImageProperty, value);
            }
            get
            {
                return (string)GetValue(SourceLeftImageProperty);
            }
        }

        public static readonly BindableProperty SourceRigthImageProperty =
            BindableProperty.Create("SourceRigthImage", typeof(string), typeof(CustomToolBar), string.Empty, BindingMode.TwoWay);
        public string SourceRigthImage
        {
            set
            {
                SetValue(SourceRigthImageProperty, value);
            }
            get
            {
                return (string)GetValue(SourceRigthImageProperty);
            }
        }

        public static readonly BindableProperty IsVisibleRigthButtonProperty =
            BindableProperty.Create("IsVisibleRigthButton", typeof(bool), typeof(CustomToolBar), true, BindingMode.TwoWay);
        public bool IsVisibleRigthButton
        {
            set
            {
                SetValue(IsVisibleRigthButtonProperty, value);
            }
            get
            {
                return (bool)GetValue(IsVisibleRigthButtonProperty);
            }
        }

        public CustomToolBar()
        {
            InitializeComponent();
        }
    }
}
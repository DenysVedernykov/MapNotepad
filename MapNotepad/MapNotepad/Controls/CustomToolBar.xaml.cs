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
        #region -- Public properties --
        public static readonly BindableProperty LeftButtonCommandProperty = BindableProperty.Create(
            propertyName: nameof(LeftButtonCommand),
            returnType: typeof(ICommand),
            declaringType: typeof(CustomToolBar),
            defaultValue: null,
            defaultBindingMode: BindingMode.TwoWay);
        public ICommand LeftButtonCommand
        {
            set => SetValue(LeftButtonCommandProperty, value);
            get => (ICommand)GetValue(LeftButtonCommandProperty);
        }

        public static readonly BindableProperty RigthButtonCommandProperty = BindableProperty.Create(
            propertyName: nameof(RigthButtonCommand),
            returnType: typeof(ICommand),
            declaringType: typeof(CustomToolBar),
            defaultValue: null,
            defaultBindingMode: BindingMode.TwoWay);
        public ICommand RigthButtonCommand
        {
            set => SetValue(RigthButtonCommandProperty, value);
            get => (ICommand)GetValue(RigthButtonCommandProperty);
        }

        public static readonly BindableProperty TitleProperty = BindableProperty.Create(
            propertyName: nameof(Title),
            returnType: typeof(string),
            declaringType: typeof(CustomToolBar),
            defaultValue: string.Empty,
            defaultBindingMode: BindingMode.TwoWay);
        public string Title
        {
            set => SetValue(TitleProperty, value);
            get => (string)GetValue(TitleProperty);
        }

        public static readonly BindableProperty TitleColorProperty = BindableProperty.Create(
            propertyName: nameof(TitleColor),
            returnType: typeof(Color),
            declaringType: typeof(CustomToolBar),
            defaultValue: Color.Black,
            defaultBindingMode: BindingMode.TwoWay);
        public Color TitleColor
        {
            set => SetValue(TitleColorProperty, value);
            get => (Color)GetValue(TitleColorProperty);
        }

        public static readonly BindableProperty FontFamilyProperty = BindableProperty.Create(
            propertyName: nameof(FontFamily),
            returnType: typeof(string),
            declaringType: typeof(CustomToolBar),
            defaultValue: string.Empty,
            defaultBindingMode: BindingMode.TwoWay);
        public string FontFamily
        {
            set => SetValue(FontFamilyProperty, value);
            get => (string)GetValue(FontFamilyProperty);
        }

        public static readonly BindableProperty BackColorProperty = BindableProperty.Create(
            propertyName: nameof(BackColor),
            returnType: typeof(Color),
            declaringType: typeof(CustomToolBar),
            defaultValue: Color.White,
            defaultBindingMode: BindingMode.TwoWay);
        public Color BackColor
        {
            set => SetValue(BackColorProperty, value);
            get => (Color)GetValue(BackColorProperty);
        }

        public static readonly BindableProperty SourceLeftImageProperty = BindableProperty.Create(
            propertyName: nameof(SourceLeftImage),
            returnType: typeof(string),
            declaringType: typeof(CustomToolBar),
            defaultValue: string.Empty,
            defaultBindingMode: BindingMode.TwoWay);
        public string SourceLeftImage
        {
            set => SetValue(SourceLeftImageProperty, value);
            get => (string)GetValue(SourceLeftImageProperty);
        }

        public static readonly BindableProperty SourceRigthImageProperty = BindableProperty.Create(
            propertyName: nameof(SourceRigthImage),
            returnType: typeof(string),
            declaringType: typeof(CustomToolBar),
            defaultValue: string.Empty,
            defaultBindingMode: BindingMode.TwoWay);
        public string SourceRigthImage
        {
            set => SetValue(SourceRigthImageProperty, value);
            get => (string)GetValue(SourceRigthImageProperty);
        }

        public static readonly BindableProperty IsVisibleRigthButtonProperty = BindableProperty.Create(
            propertyName: nameof(IsVisibleRigthButton),
            returnType: typeof(bool),
            declaringType: typeof(CustomToolBar),
            defaultValue: true,
            defaultBindingMode: BindingMode.TwoWay);
        public bool IsVisibleRigthButton
        {
            set => SetValue(IsVisibleRigthButtonProperty, value);
            get => (bool)GetValue(IsVisibleRigthButtonProperty);
        }

        #endregion

        public CustomToolBar()
        {
            InitializeComponent();
        }
    }
}
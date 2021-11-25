using Xamarin.Forms;

namespace MapNotepad.Controls
{
    public partial class CustomTabbedPage : TabbedPage
    {
        public CustomTabbedPage()
        {
            InitializeComponent();
        }

        #region -- Public properties --

        public static readonly BindableProperty BackgroundSelectedTabProperty = BindableProperty.Create(
            propertyName: nameof(BackgroundSelectedTab),
            returnType: typeof(Color),
            declaringType: typeof(CustomTabbedPage),
            defaultBindingMode: BindingMode.TwoWay);

        public Color BackgroundSelectedTab
        {
            set => SetValue(BackgroundSelectedTabProperty, value);
            get => (Color)GetValue(BackgroundSelectedTabProperty);
        }

        public static readonly BindableProperty SelectedTabProperty = BindableProperty.Create(
            propertyName: nameof(SelectedTab),
            returnType: typeof(int),
            declaringType: typeof(CustomTabbedPage),
            defaultBindingMode: BindingMode.TwoWay);

        public int SelectedTab
        {
            set => SetValue(SelectedTabProperty, value);
            get => (int)GetValue(SelectedTabProperty);
        }

        #endregion

        #region -- Overrides --

        protected override void OnPropertyChanged(string propertyName)
        {
            base.OnPropertyChanged(propertyName);

            switch (propertyName)
            {
                case nameof(SelectedTab):

                    if(SelectedTab >= 0 && SelectedTab < Children.Count)
                    {
                        CurrentPage = Children[SelectedTab];
                    }

                    break;
            }
        }

        protected override void OnCurrentPageChanged()
        {
            base.OnCurrentPageChanged();

            for(int i = 0; i < Children.Count; i++)
            {
                if (CurrentPage == Children[i])
                {
                    SelectedTab = i;
                    break;
                }
            }
        }

        #endregion

    }
}
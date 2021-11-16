using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MapNotepad.Controls
{
    public partial class CustomTabbedPage : TabbedPage
    {
        public CustomTabbedPage()
        {
            InitializeComponent();
        }

        #region -- Public properties --

        public static readonly BindableProperty SelectedTabProperty = BindableProperty.Create(
            propertyName: nameof(SelectedTab),
            returnType: typeof(int),
            declaringType: typeof(CustomTabbedPage),
            defaultValue: 0,
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
                    if(SelectedTab >= 0 && SelectedTab < this.Children.Count)
                    {
                        this.CurrentPage = this.Children[SelectedTab];
                    }
                    break;
            }
        }

        protected override void OnCurrentPageChanged()
        {
            base.OnCurrentPageChanged();

            for(int i = 0; i < this.Children.Count; i++)
            {
                if (this.CurrentPage == this.Children[i])
                {
                    SelectedTab = i;
                    break;
                }
            }
        }

        #endregion

    }
}
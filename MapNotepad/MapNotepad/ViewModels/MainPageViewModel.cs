using Prism.Navigation;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MapNotepad.ViewModels
{
    class MainPageViewModel : BaseViewModel
    {
        public MainPageViewModel(
            INavigationService navigationService)
            : base(navigationService)
        {
            
        }

        #region -- Public properties --

        private int _selectedTab;
        public int SelectedTab
        {
            get => _selectedTab;
            set => SetProperty(ref _selectedTab, value);
        }

        #endregion

        #region -- IInitializeAsync implementation --

        public async override Task InitializeAsync(INavigationParameters parameters)
        {
            MessagingCenter.Subscribe<PinsPageViewModel, int>(
                this,
                "SwitchTab",
                (sender, item) => {
                    SelectedTab = item;
                });
        }

        #endregion

    }
}

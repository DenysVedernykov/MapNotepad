using MapNotepad.Helpers;
using MapNotepad.Models;
using MapNotepad.Views;
using Prism.Navigation;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms.GoogleMaps;

namespace MapNotepad.ViewModels
{
    class MapPageViewModel : BaseViewModel
    {
        public MapPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            All = new ObservableCollection<UserPin>
            {
                new UserPin
                {
                    Label = "Chimpanzee",
                    Description = "Hominidae"
                }
            };

            IsShowList = false;
        }

        #region -- Public properties --

        private string _text;
        public string Text
        {
            get => _text;
            set => SetProperty(ref _text, value);
        }

        private bool _isShowList;
        public bool IsShowList
        {
            get => _isShowList;
            set => SetProperty(ref _isShowList, value);
        }

        public ObservableCollection<UserPin> All { get; }

        public ObservableCollection<Pin> Pins { get; set; }

        private ICommand _itemTappedCommand;
        public ICommand ItemTappedCommand => _itemTappedCommand ??= SingleExecutionCommand.FromFunc(OnItemTappedCommandAsync);

        private ICommand _exitButtonCommand;
        public ICommand ExitButtonCommand => _exitButtonCommand ??= SingleExecutionCommand.FromFunc(OnExitButtonCommandAsync);
        
        private ICommand _GoSettingsButtonCommand;
        public ICommand GoSettingsButtonCommand => _GoSettingsButtonCommand ??= SingleExecutionCommand.FromFunc(OnGoSettingsButtonCommandAsync);

        #endregion

        #region -- IInitializeAsync implementation --

        public async override Task InitializeAsync(INavigationParameters parameters)
        {
            Pins.Add(new Pin()
            {
                Label = "Tokyo SKYTREE 1",
                Position = new Position(35.71d, 139.99d)
            });

            Pins.Add(new Pin()
            {
                Label = "Tokyo SKYTREE 2",
                Position = new Position(35.71d, 139.91d)
            });

            Pins.Add(new Pin()
            {
                Type = PinType.Generic,
                Label = "Tokyo SKYTREE 3",
                Address = "Sumida-ku, Tokyo, Japan",
                Position = new Position(35.71d, 139.81d),
                Tag = "id_tokyo",
                IsVisible = true
            });
        }

        #endregion

        #region -- Private methods --

        private Task OnItemTappedCommandAsync()
        {
            IsShowList = false;
            Text = "";

            return Task.CompletedTask;
        }

        private Task OnExitButtonCommandAsync()
        {
            

            return Task.CompletedTask;
        }

        private Task OnGoSettingsButtonCommandAsync()
        {
            _navigationService.NavigateAsync(nameof(SettingsPage));

            return Task.CompletedTask;
        }

        #endregion

    }
}
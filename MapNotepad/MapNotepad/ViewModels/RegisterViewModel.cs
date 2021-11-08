using MapNotepad.Helpers;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace MapNotepad.ViewModels
{
    class RegisterViewModel : BaseViewModel
    {
        public RegisterViewModel(INavigationService navigationService) : base(navigationService)
        {
            Name = Resource.ResourceManager.GetString("CreateAnAccount", Resource.Culture);
        }

        #region -- Overrides --

        protected override void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            base.OnPropertyChanged(args);

            switch (args.PropertyName)
            {
                case nameof(Name):

                    break;
            }
        }

        #endregion

        #region -- Public properties --

        private string _name;
        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        private ICommand _goToBackCommand;
        public ICommand GoToBackCommand => _goToBackCommand ??= SingleExecutionCommand.FromFunc(OnGoToBackCommandAsync);
        private Task OnGoToBackCommandAsync()
        {
            _navigationService.GoBackAsync();

            return Task.CompletedTask;
        }

        #endregion
    }
}
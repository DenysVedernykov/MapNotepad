using MapNotepad.Models;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace MapNotepad.ViewModels
{
    class MapPageViewModel:BaseViewModel
    {
        public MapPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            All = new ObservableCollection<UserPin>
            {
                new UserPin
                {
                    Title = "Chimpanzee",
                    Description = "Hominidae"
                },
                new UserPin
                {
                    Title = "Orangutan",
                    Description = "Hominidae"
                },
                new UserPin
                {
                    Title = "Tamarin",
                    Description = "Callitrichidae"
                }
            };
        }

        public ObservableCollection<UserPin> All { get; }
        
    }
}

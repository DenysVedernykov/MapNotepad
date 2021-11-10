using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MapNotepad.ViewModels
{
    public class BaseViewModel : BindableBase, INavigationAware, IInitializeAsync
    {
        protected INavigationService _navigationService;

        public BaseViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        #region -- IInitializeAsync implementation --

        public async virtual Task InitializeAsync(INavigationParameters parameters)
        {

        }

        #endregion

        #region -- INavigationAware implementation --

        public virtual void OnNavigatedFrom(INavigationParameters parameters)
        {
            
        }

        public virtual void OnNavigatedTo(INavigationParameters parameters)
        {
            
        }

        #endregion

    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Acr.UserDialogs;
using MvvmCross.ViewModels;
using WeatherMobileApp.Core.Exceptions;
using Xamarin.Essentials;

namespace WeatherMobileApp.Core.ViewModels
{
    public abstract class BaseViewModel : MvxViewModel
    {
        private readonly IUserDialogs _dialogs;

        public BaseViewModel()
        {
            _dialogs = UserDialogs.Instance;
        }

        public BaseViewModel(IUserDialogs dialogs)
        {
            _dialogs = dialogs;
        }

        public async Task LoadingCommand(Func<Task> action, string loadingTitle = null)
        {
            _dialogs.ShowLoading(loadingTitle);

            await action();

            _dialogs.HideLoading();
        }

        public async Task LoadingCommand(Func<Delegate, Task> action, string loadingTitle = null)
        {
            _dialogs.ShowLoading(loadingTitle);

            await action(new Action(_dialogs.HideLoading));

            _dialogs.HideLoading();
        }

        public bool CheckConnection()
        {
            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
                return true;
            throw new InternetConnectionException();
        }

        public void ShowAlert(string title, string message)
        {
            _dialogs.Alert(message, title);
        }
    }
}

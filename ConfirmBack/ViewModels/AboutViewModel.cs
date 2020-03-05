using System;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace ConfirmBack.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        public AboutViewModel()
        {
            Title = "About";
            OpenWebCommand = new Command(async () => await Browser.OpenAsync("https://xamarin.com"));
            ToggleNavCommand = new Command(() => ((ConfirmBack.App)App.Current).ToggleNavigation());
            ToggleConfirmationStyleCommand = new Command(() =>
            {
                var app = (ConfirmBack.App)App.Current;
                app.IsToastExitConfirmation = !app.IsToastExitConfirmation;
            });
        }

        public ICommand OpenWebCommand { get; }
        public ICommand ToggleNavCommand { get; }
        public ICommand ToggleConfirmationStyleCommand { get; }
    }
}
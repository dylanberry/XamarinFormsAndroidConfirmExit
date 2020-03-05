using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ConfirmBack.Services;
using ConfirmBack.Views;
using Xamarin.Essentials;

namespace ConfirmBack
{
    public partial class App : Application
    {
        public bool PromptToConfirmExit
        {
            get
            {
                bool promptToConfirmExit = false;
                if (MainPage is ContentPage)
                {
                    promptToConfirmExit = true;
                }
                else if (MainPage is Xamarin.Forms.MasterDetailPage masterDetailPage
                    && masterDetailPage.Detail is NavigationPage detailNavigationPage)
                {
                    promptToConfirmExit = detailNavigationPage.Navigation.NavigationStack.Count <= 1;
                }
                else if (MainPage is NavigationPage mainPage)
                {
                    if (mainPage.CurrentPage is TabbedPage tabbedPage
                        && tabbedPage.CurrentPage is NavigationPage navigationPage)
                    {
                        promptToConfirmExit = navigationPage.Navigation.NavigationStack.Count <= 1;
                    }
                    else
                    {
                        promptToConfirmExit = mainPage.Navigation.NavigationStack.Count <= 1;
                    }
                }
                else if (MainPage is TabbedPage tabbedPage
                    && tabbedPage.CurrentPage is NavigationPage navigationPage)
                {
                    promptToConfirmExit = navigationPage.Navigation.NavigationStack.Count <= 1;
                }
                return promptToConfirmExit;
            }
        }

        public App()
        {
            InitializeComponent();

            DependencyService.Register<MockDataStore>();
            MainPage = new TabNavPage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }

        public void ToggleNavigation()
        {
            if (MainPage is TabNavPage)
                MainPage = new MasterDetailNavPage();
            else
                MainPage = new TabNavPage();
        }

        public bool IsToastExitConfirmation
        {
            get => Preferences.Get(nameof(IsToastExitConfirmation), false);
            set => Preferences.Set(nameof(IsToastExitConfirmation), value);
        }
    }
}

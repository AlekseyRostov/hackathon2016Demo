using Feedback.UI.Core.Views.Authentication;

namespace Feedback.UI.Core
{
    public partial class App
    {
        public App()
        {
            InitializeComponent();
            //MainPage = new NavigationPage(new PlacesPage());
            MainPage = new LoginPage();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
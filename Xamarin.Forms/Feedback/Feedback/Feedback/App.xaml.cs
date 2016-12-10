using Feedback.Core.Entities;
using Feedback.Core.Services;
using Feedback.UI.Core.Views.Authentication;
using Feedback.UI.Core.Views.Places;
using Microsoft.Practices.Unity;
using PubSub;
using Xamarin.Forms;

namespace Feedback.UI.Core
{
    public partial class App
    {
        private readonly IAuthenticationService _authenticationService;
        private NavigationPage _rootPage;

        public App()
        {
            InitializeComponent();
            _authenticationService = ServiceLocator.Instance.Resolve<IAuthenticationService>();
            _authenticationService.RestoreSession();
            SetupMainPage();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
            this.Subscribe<User>(CurrentUserChanged);
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
            this.Unsubscribe<User>();
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
            SetupMainPage();
            this.Subscribe<User>(CurrentUserChanged);
        }

        private void CurrentUserChanged(User user)
        {
            SetupMainPage();
        }

        private void SetupMainPage()
        {
            if(_authenticationService.CurrentUser != null)
            {
                if(MainPage == null || MainPage != _rootPage)
                {
                    _rootPage = _rootPage ?? new NavigationPage(new PlacesPage());
                    MainPage = _rootPage;
                }
            }
            else
            {
                if(!(MainPage is LoginPage))
                {
                    MainPage = new LoginPage();
                }
            }
        }
    }
}
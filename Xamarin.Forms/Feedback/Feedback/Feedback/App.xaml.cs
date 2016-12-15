using System;
using System.Diagnostics;
using Feedback.Core.Entities;
using Feedback.Core.Services;
using Feedback.UI.Core.Views.Authentication;
using Feedback.UI.Core.Views.Feedbacks;
using Feedback.UI.Core.Views.Places;
using Feedback.UI.Services;
using Microsoft.Practices.Unity;
using PubSub;
using Xamarin.Forms;

namespace Feedback.UI.Core
{
    public partial class App
    {
        private readonly IAuthenticationService _authenticationService;
        private NavigationPage _rootPage;
        private IBeaconLocationService _beaconLocationService;
        private IPlaceService _placeService;

        public App()
        {
            InitializeComponent();
            _authenticationService = ServiceLocator.Instance.Resolve<IAuthenticationService>();
            _authenticationService.RestoreSession();
            Setup();
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
            Setup();
            this.Subscribe<User>(CurrentUserChanged);
        }

        private void CurrentUserChanged(User user)
        {
            Setup();
        }

        private void Setup()
        {
            if(_authenticationService.CurrentUser != null)
            {
                if(MainPage == null || MainPage != _rootPage)
                {
                    _rootPage = _rootPage ?? new NavigationPage(new PlacesPage());
                    MainPage = _rootPage;
                }
                SetupBeacons(true);
            }
            else
            {
                SetupBeacons(false);
                if(!(MainPage is LoginPage))
                {
                    MainPage = new LoginPage();
                }
            }
        }

        private void SetupBeacons(bool activate)
        {
            if(activate)
            {
                _beaconLocationService = ServiceLocator.Instance.Resolve<IBeaconLocationService>();
                _beaconLocationService.BeaconFound += OnBeaconFound;
                _beaconLocationService.StartMonitoring(new[] {new BeaconModel
                                                              {
                                                                  UUID = "6BF6DBA4-6D12-4C42-AE68-5344159683E3",
                                                                  Minor = 1,
                                                                  Major = 8
                                                              }});
            }
            else if(_beaconLocationService != null)
            {
                _beaconLocationService.BeaconFound -= OnBeaconFound;
                _beaconLocationService.StopMonitoring();
                _beaconLocationService = null;
            }
        }

        private async void OnBeaconFound(object sender, BeaconModel beaconModel)
        {
            try
            {
                _placeService = _placeService ?? ServiceLocator.Instance.Resolve<IPlaceService>();
                var place = await _placeService.GetPlaceByBeaconAsync(beaconModel);
                var feedbackPage = _rootPage.CurrentPage as FeedbackPage;
                if(feedbackPage == null || feedbackPage.ViewModel?.PlaceId != place.Id)
                {
                    await _rootPage.PushAsync(new FeedbackPage(place.Id, place.Name));
                }
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }
    }
}
using System;
using Feedback.API.Services;
using Feedback.API.Services.Implementations;
using Feedback.Core.Services;
using Feedback.Core.Services.Implementations;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using Feedback.Core.ViewModels.Places;
using Feedback.Core.ViewModels.Login;
using Feedback.API.Entities;
using MvvmCross.Platform.Plugins;
using MvvmCross.Plugins.Messenger;
using Feedback.Core.Messages;
using Feedback.Core.ViewModels.Feedbacks.Feedback;

namespace Feedback.Core
{
    public class App : MvxNavigatingObject, IMvxApplication
    {
        #region Fields

        private MvxSubscriptionToken _userChangedToken;

        private bool _feedbackWasShown;

        #endregion

        #region Services

        private IBeaconLocationService _beaconLocationService;
        private IBeaconLocationService BeaconLocationService
        {
            get
            {
                return _beaconLocationService ?? (_beaconLocationService = Mvx.Resolve<IBeaconLocationService>());
            }
        }

        private IPlaceService _placeService;
        private IPlaceService PlaceService
        {
            get
            {
                return _placeService ?? (_placeService = Mvx.Resolve<IPlaceService>());
            }
        }

        private IMvxViewModelLocator _defaultLocator;
        private IMvxViewModelLocator DefaultLocator
        {
            get
            {
                return _defaultLocator ?? (_defaultLocator = CreateDefaultViewModelLocator());
            }
        }

        #endregion

        #region Private

        private void Setup()
        {
            if (Mvx.Resolve<IAuthenticationService>().CurrentUser != null)
            {
                Mvx.RegisterSingleton<IMvxAppStart>(new MvxAppStart<PlacesViewModel>());

                SetupBeacons(true);
            }
            else
            {
                SetupBeacons(false);

                Mvx.RegisterSingleton<IMvxAppStart>(new MvxAppStart<LoginViewModel>());
            }
        }

        private void SetupBeacons(bool activate)
        {
            if (activate)
            {
                BeaconLocationService.BeaconFound += OnBeaconFound;
                BeaconLocationService.StartMonitoring(new[]
                {
                    new BeaconModel
                    {
                        UUID = "6BF6DBA4-6D12-4C42-AE68-5344159683E3",
                        Major = 1,
                        Minor = 8
                    }
                });
            }
            else
            {
                BeaconLocationService.BeaconFound -= OnBeaconFound;
                BeaconLocationService.StopMonitoring();
            }
        }

        private async void OnBeaconFound(object sender, BeaconModel beaconModel)
        {
            try
            {
                var place = await PlaceService.GetPlaceByBeaconAsync(beaconModel);

                if (!_feedbackWasShown)
                {
                    _feedbackWasShown = true;
                    ShowViewModel<FeedbackViewModel>(new { id = place.Id, name = place.Name }); 
                }
                else
                    Mvx.Resolve<IMvxMessenger>().Publish(new BeaconFoundMessage(this, place.Id, place.Name));
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }
        }

        private void CurrentUserChanged(UserChangedMessage msg)
        {
            if (Mvx.Resolve<IAuthenticationService>().CurrentUser != null)
            {
                ShowViewModel<PlacesViewModel>();

                SetupBeacons(true);
            }
            else
            {
                SetupBeacons(false);

                ShowViewModel<LoginViewModel>();
            }
        }

        #endregion

        #region Protected

        protected virtual IMvxViewModelLocator CreateDefaultViewModelLocator()
        {
            return new MvxDefaultViewModelLocator();
        }

        #endregion

        #region IMvxApplication implementation

        public void Initialize()
        {
            #region API services

            Mvx.RegisterType<ICognitiveServiceAuthenticator, CognitiveServiceAuthenticator>();
            Mvx.RegisterType<IFeedbackService, FeedbackService>();
            Mvx.RegisterType<IPlaceService, PlaceService>();

            #endregion

            #region Core services

            Mvx.RegisterType<ISpeechService, BingSpeechService>();

            #endregion

            Mvx.Resolve<IAuthenticationService>().RestoreSession();

            Setup();

            _userChangedToken = Mvx.Resolve<IMvxMessenger>().Subscribe<UserChangedMessage>(CurrentUserChanged);
        }

        public void LoadPlugins(IMvxPluginManager pluginManager)
        {
            //nothing
        }

        public IMvxViewModelLocator FindViewModelLocator(MvxViewModelRequest request)
        {
            return this.DefaultLocator;
        }

        #endregion
    }
}

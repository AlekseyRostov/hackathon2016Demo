using System;
using Feedback.API.Services;
using Feedback.API.Services.Implementations;
using Feedback.Core.Services;
using Feedback.Core.Services.Implementations;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;

namespace Feedback.Core
{
    public class App : MvxApplication
    {
        public override void Initialize()
        {
            #region API services

            Mvx.RegisterType<ICognitiveServiceAuthenticator, CognitiveServiceAuthenticator>();
            Mvx.RegisterType<IFeedbackService, FeedbackService>();
            Mvx.RegisterType<IPlaceService, PlaceService>();

            #endregion

            #region Core services

            Mvx.RegisterType<ISpeechService, BingSpeechService>();

            #endregion
        }
    }
}

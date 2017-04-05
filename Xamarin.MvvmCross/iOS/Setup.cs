using MvvmCross.Core.ViewModels;
using MvvmCross.iOS.Platform;
using MvvmCross.iOS.Views.Presenters;
using MvvmCross.Platform.Platform;
using UIKit;
using MvvmCross.Platform;
using Feedback.Core.Services;
using Feedback.iOS.Services;
using Feedback.iOS.Presenter;

namespace Feedback.iOS
{
    public class Setup : MvxIosSetup
    {
        public Setup(MvxApplicationDelegate applicationDelegate, UIWindow window)
            : base(applicationDelegate, window)
        {
        }
        
        public Setup(MvxApplicationDelegate applicationDelegate, IMvxIosViewPresenter presenter)
            : base(applicationDelegate, presenter)
        {
        }

        protected override IMvxApplication CreateApp()
        {
            return new Core.App();
        }
        
        protected override IMvxTrace CreateDebugTrace()
        {
            return new FeedbackDebugTrace();
        }

        protected override void InitializeFirstChance()
        {
            base.InitializeFirstChance();

            Mvx.RegisterSingleton<IAuthenticationService>(() => new AuthenticationService());

            Mvx.RegisterType<IAudioRecorderService, AudioRecorderService>();
            Mvx.RegisterType<IDeviceService, DeviceService>();
            Mvx.RegisterType<IBeaconLocationService, BeaconLocationService>();
        }
    }
}

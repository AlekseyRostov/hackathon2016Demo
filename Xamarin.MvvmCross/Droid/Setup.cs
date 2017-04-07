using Android.Content;
using MvvmCross.Droid.Platform;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform.Platform;
using MvvmCross.Platform;
using Feedback.Core.Services;
using Feedback.Droid.Services;

namespace Feedback.Droid
{
    public class Setup : MvxAndroidSetup
    {
        public Setup(Context applicationContext) : base(applicationContext)
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
            Mvx.RegisterSingleton<IBeaconLocationService>(() => new BeaconLocationService());

            Mvx.RegisterType<IAudioRecorderService, AudioRecorderService>();
            Mvx.RegisterType<IDeviceService, DeviceService>();
        }
    }
}

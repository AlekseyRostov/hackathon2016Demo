using Android.App;
using Feedback.Core.ViewModels.Feedbacks.Feedback;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Droid.Support.V7.AppCompat;

namespace Feedback.Droid.Views.Feedbacks.Feedback
{
    [Activity]
    public class FeedbackActivity : MvxAppCompatActivity<IFeedbackViewModel>
    {
        protected override void OnCreate(Android.OS.Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.FeedbackView);

            var set = this.CreateBindingSet<FeedbackActivity, IFeedbackViewModel>();
            set.Bind().For(Title).To(vm => vm.PlaceName);
            set.Apply();
        }
    }
}

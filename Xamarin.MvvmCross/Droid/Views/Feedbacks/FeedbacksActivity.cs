using Android.App;
using Feedback.Core.ViewModels.Feedbacks;
using MvvmCross.Droid.Support.V7.AppCompat;
using MvvmCross.Binding.BindingContext;

namespace Feedback.Droid.Views.Feedbacks
{
    [Activity]
    public class FeedbacksActivity : MvxAppCompatActivity<IFeedbacksViewModel>
    {
        protected override void OnCreate(Android.OS.Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.FeedbacksView);

            var set = this.CreateBindingSet<FeedbacksActivity, IFeedbacksViewModel>();
            set.Bind().For(Title).To(vm => vm.PlaceName);
            set.Apply();
        }
    }
}

using System;
using MvvmCross.Droid.Support.V4;
using Feedback.Core.ViewModels.Feedbacks.Feedback;
using MvvmCross.Droid.Support.V7.AppCompat;
using Android.App;
namespace Feedback.Droid.Views.Feedbacks.Feedback
{
    [Activity]
    public class FeedbackActivity : MvxAppCompatActivity<IFeedbackViewModel>
    {
        protected override void OnCreate(Android.OS.Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.FeedbackView);
        }
    }
}

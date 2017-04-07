using System;
using Feedback.Core.ViewModels.Feedbacks;
using MvvmCross.Droid.Support.V4;
using MvvmCross.Droid.Support.V7.AppCompat;
using Android.App;

namespace Feedback.Droid.Views.Feedbacks
{
    [Activity]
    public class FeedbacksActivity : MvxAppCompatActivity<IFeedbacksViewModel>
    {
        protected override void OnCreate(Android.OS.Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.FeedbacksView);
        }
    }
}

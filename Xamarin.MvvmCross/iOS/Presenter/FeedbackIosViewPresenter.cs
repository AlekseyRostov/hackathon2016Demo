using System;
using MvvmCross.iOS.Views.Presenters;
using UIKit;
namespace Feedback.iOS.Presenter
{
    public class FeedbackIosViewPresenter : MvxIosViewPresenter
    {
        public FeedbackIosViewPresenter(UIApplicationDelegate appDelegate, UIWindow window)
        : base (appDelegate, window)
        {
        }
    }
}

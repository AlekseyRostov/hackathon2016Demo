using System;

using Foundation;
using MvvmCross.Binding.iOS.Views;
using UIKit;
using MvvmCross.Binding.BindingContext;

namespace Feedback.iOS.Views.Feedbacks.Cells
{
    public partial class FeedbacksCell : MvxTableViewCell
    {
        public static readonly NSString Key = new NSString("FeedbacksCell");
        public static readonly UINib Nib;

        static FeedbacksCell()
        {
            Nib = UINib.FromName("FeedbacksCell", NSBundle.MainBundle);
        }

        protected FeedbacksCell(IntPtr handle) 
            : base(handle)
        {
            this.DelayBind(() =>
            {
                var set = this.CreateBindingSet<FeedbacksCell, API.Entities.Feedback>();
                set.Bind(TextLabel).To(vm => vm.Text);
                set.Bind(DetailTextLabel).To(vm => vm.UserEmail);
                set.Apply();
            });
        }
    }
}

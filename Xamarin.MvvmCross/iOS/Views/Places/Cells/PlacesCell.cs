using MvvmCross.Binding.iOS.Views;

using System;

using Foundation;
using UIKit;
using MvvmCross.Binding.BindingContext;
using Feedback.API.Entities;

namespace Feedback.iOS.Views.Places.Cells
{
    public partial class PlacesCell : MvxTableViewCell
    {
        public static readonly NSString Key = new NSString("PlacesCell");
        public static readonly UINib Nib;

        static PlacesCell()
        {
            Nib = UINib.FromName("PlacesCell", NSBundle.MainBundle);
        }

        protected PlacesCell(IntPtr handle) 
            : base(handle)
        {
            this.DelayBind(() =>
            {
                TextLabel.TextColor = UIColor.Black;

                var set = this.CreateBindingSet<PlacesCell, Place>();
                set.Bind(TextLabel).To(vm => vm.Name);
                set.Apply();
            });
        }
    }
}

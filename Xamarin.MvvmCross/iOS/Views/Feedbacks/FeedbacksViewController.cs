using System;
using Feedback.iOS.Views.Feedbacks.Cells;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Binding.iOS.Views;
using MvvmCross.iOS.Views;
using UIKit;
using Feedback.Core.ViewModels.Feedbacks;
using Feedback.Core.ViewModels.Feedbacks.Feedback;

namespace Feedback.iOS.Views.Feedbacks
{
    public partial class FeedbacksViewController : MvxViewController<IFeedbackViewModel>
    {
        public FeedbacksViewController() 
            : base("FeedbacksViewController", null)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            var barButtonItem = new UIBarButtonItem("Add", UIBarButtonItemStyle.Done, null);

            var dataSource = new MvxSimpleTableViewSource(_tableView, FeedbacksCell.Key, FeedbacksCell.Key);

            _tableView.RegisterNibForCellReuse(FeedbacksCell.Nib, FeedbacksCell.Key);
            _tableView.Source = dataSource;
            _tableView.RowHeight = UITableView.AutomaticDimension;
            _tableView.EstimatedRowHeight = 44;

            var set = this.CreateBindingSet<FeedbacksViewController, IFeedbacksViewModel>();

            set.Bind().For("Title").To(vm => vm.PlaceName);

            set.Bind(dataSource).To(vm => vm.Feedbacks);

            set.Bind(_tableLayout).For("Visibility").To(vm => vm.IsLoaded).WithConversion("Visibility");

            set.Bind(_tableView).For("Visibility").To(vm => vm.IsEmpty).WithConversion("InvertedVisibility");

            set.Bind(_tableLabel).For("Visibility").To(vm => vm.IsEmpty).WithConversion("Visibility");

            set.Bind(_loading).For("Visibility").To(vm => vm.IsLoading).WithConversion("Visibility");

            set.Bind(_failedLabel).To(vm => vm.LoadFailureMessage);
            set.Bind(_failedLabel).For("Visibility").To(vm => vm.LoadFailureMessage).WithConversion("Visibility");

            set.Bind(barButtonItem).To(vm => vm.AddCommand);

            set.Apply();

            _tableView.ReloadData();


            NavigationItem.SetRightBarButtonItem(barButtonItem, false);
        }
    }
}


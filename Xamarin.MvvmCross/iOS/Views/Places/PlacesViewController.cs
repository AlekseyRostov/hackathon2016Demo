using System;
using MvvmCross.iOS.Views;
using UIKit;
using Feedback.Core.ViewModels.Places;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Binding.iOS.Views;
using Feedback.iOS.Views.Places.Cells;

namespace Feedback.iOS.Views.Places
{
    public partial class PlacesViewController : MvxViewController<IPlacesViewModel>
    {
        public PlacesViewController() 
            : base("PlacesViewController", null)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            var barButtonItem = new UIBarButtonItem("Logout", UIBarButtonItemStyle.Done, null);

            var dataSource = new MvxSimpleTableViewSource(_tableView, PlacesCell.Key, PlacesCell.Key);

            _tableView.RegisterNibForCellReuse(PlacesCell.Nib, PlacesCell.Key);
            _tableView.Source = dataSource;


            var set = this.CreateBindingSet<PlacesViewController, IPlacesViewModel>();

            set.Bind(dataSource).To(vm => vm.Places);
            set.Bind(dataSource).For(ds => ds.SelectionChangedCommand).To(vm => vm.SelectionChangedCommand);

            set.Bind(_tableLayout).For("Visibility").To(vm => vm.IsLoaded).WithConversion("Visibility");

            set.Bind(_tableView).For("Visibility").To(vm => vm.IsEmpty).WithConversion("InvertedVisibility");

            set.Bind(_tableLabel).For("Visibility").To(vm => vm.IsEmpty).WithConversion("Visibility");

            set.Bind(_loading).For("Visibility").To(vm => vm.IsLoading).WithConversion("Visibility");

            set.Bind(_failedLabel).To(vm => vm.LoadFailureMessage);
            set.Bind(_failedLabel).For("Visibility").To(vm => vm.LoadFailureMessage).WithConversion("Visibility");

            set.Bind(barButtonItem).To(vm => vm.LogoutCommand);

            set.Apply();

            _tableView.ReloadData();


            NavigationItem.SetRightBarButtonItem(barButtonItem, false);
        }
    }
}


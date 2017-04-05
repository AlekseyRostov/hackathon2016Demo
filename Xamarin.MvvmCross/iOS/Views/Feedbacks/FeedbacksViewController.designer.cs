// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace Feedback.iOS.Views.Feedbacks
{
	[Register ("FeedbacksViewController")]
	partial class FeedbacksViewController
	{
		[Outlet]
		UIKit.UILabel _failedLabel { get; set; }

		[Outlet]
		UIKit.UIActivityIndicatorView _loading { get; set; }

		[Outlet]
		UIKit.UILabel _tableLabel { get; set; }

		[Outlet]
		UIKit.UIView _tableLayout { get; set; }

		[Outlet]
		UIKit.UITableView _tableView { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (_failedLabel != null) {
				_failedLabel.Dispose ();
				_failedLabel = null;
			}

			if (_loading != null) {
				_loading.Dispose ();
				_loading = null;
			}

			if (_tableLabel != null) {
				_tableLabel.Dispose ();
				_tableLabel = null;
			}

			if (_tableLayout != null) {
				_tableLayout.Dispose ();
				_tableLayout = null;
			}

			if (_tableView != null) {
				_tableView.Dispose ();
				_tableView = null;
			}
		}
	}
}

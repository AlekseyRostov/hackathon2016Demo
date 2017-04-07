// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace Feedback.iOS.Views.Feedbacks.Feedback
{
	[Register ("FeedbackViewController")]
	partial class FeedbackViewController
	{
		[Outlet]
		UIKit.UILabel _failedLabel { get; set; }

		[Outlet]
		UIKit.UIActivityIndicatorView _loading { get; set; }

		[Outlet]
		UIKit.UIActivityIndicatorView _processingIndicator { get; set; }

		[Outlet]
		UIKit.UIStackView _processingStackView { get; set; }

		[Outlet]
		UIKit.UIActivityIndicatorView _recordingIndicator { get; set; }

		[Outlet]
		UIKit.UIStackView _recordingStackView { get; set; }

		[Outlet]
		UIKit.UIView _speechView { get; set; }

		[Outlet]
		UIKit.UIStackView _stackView { get; set; }

		[Outlet]
		UIKit.UIButton _startRecordingBtn { get; set; }

		[Outlet]
		UIKit.UIButton _stopRecordingBtn { get; set; }

		[Outlet]
		UIKit.UITextView _textView { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (_failedLabel != null) {
				_failedLabel.Dispose ();
				_failedLabel = null;
			}

			if (_processingStackView != null) {
				_processingStackView.Dispose ();
				_processingStackView = null;
			}

			if (_processingIndicator != null) {
				_processingIndicator.Dispose ();
				_processingIndicator = null;
			}

			if (_recordingIndicator != null) {
				_recordingIndicator.Dispose ();
				_recordingIndicator = null;
			}

			if (_stopRecordingBtn != null) {
				_stopRecordingBtn.Dispose ();
				_stopRecordingBtn = null;
			}

			if (_recordingStackView != null) {
				_recordingStackView.Dispose ();
				_recordingStackView = null;
			}

			if (_startRecordingBtn != null) {
				_startRecordingBtn.Dispose ();
				_startRecordingBtn = null;
			}

			if (_speechView != null) {
				_speechView.Dispose ();
				_speechView = null;
			}

			if (_textView != null) {
				_textView.Dispose ();
				_textView = null;
			}

			if (_stackView != null) {
				_stackView.Dispose ();
				_stackView = null;
			}

			if (_loading != null) {
				_loading.Dispose ();
				_loading = null;
			}
		}
	}
}

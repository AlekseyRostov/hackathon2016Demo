using Feedback.Core.ViewModels.Feedbacks.Feedback;
using MvvmCross.Binding.BindingContext;
using MvvmCross.iOS.Views;
using UIKit;

namespace Feedback.iOS.Views.Feedbacks.Feedback
{
    public partial class FeedbackViewController : MvxViewController<IFeedbackViewModel>
    {
        public FeedbackViewController() 
            : base("FeedbackViewController", null)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            var barButtonItem = new UIBarButtonItem("Save", UIBarButtonItemStyle.Done, null);

            var set = this.CreateBindingSet<FeedbackViewController, IFeedbackViewModel>();

            set.Bind().For("Title").To(vm => vm.PlaceName);

            set.Bind(_stackView).For("Visibility").To(vm => vm.IsSaving).WithConversion("InvertedVisibility");

            set.Bind(_textView).To(vm => vm.Text);

            set.Bind(_speechView).For("Visibility").To(vm => vm.IsRecognizingSpeech).WithConversion("InvertedVisibility");

            set.Bind(_startRecordingBtn).To(vm => vm.StartRecordingCommand);
            set.Bind(_startRecordingBtn).For("Visibility").To(vm => vm.IsRecording).WithConversion("InvertedVisibility");

            set.Bind(_recordingStackView).For("Visibility").To(vm => vm.IsRecording).WithConversion("Visibility");

            set.Bind(_processingStackView).For("Visibility").To(vm => vm.IsRecognizingSpeech).WithConversion("Visibility");
            set.Bind(_processingIndicator).For("Visibility").To(vm => vm.IsRecognizingSpeech).WithConversion("Visibility");

            set.Bind(_stopRecordingBtn).To(vm => vm.StopRecordingCommand);
            set.Bind(_recordingIndicator).To(vm => vm.IsRecording);

            set.Bind(_failedLabel).To(vm => vm.SpeechRecognitionError);
            set.Bind(_failedLabel).For("Visibility").To(vm => vm.SpeechRecognitionError).WithConversion("Visibility");

            set.Bind(_loading).For("Visibility").To(vm => vm.IsSaving).WithConversion("Visibility");

            set.Bind(barButtonItem).To(vm => vm.SaveCommand);

            set.Apply();

            NavigationItem.SetRightBarButtonItem(barButtonItem, false);
        }
    }
}


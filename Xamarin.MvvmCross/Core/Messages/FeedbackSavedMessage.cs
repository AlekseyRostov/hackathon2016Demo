using System;
using MvvmCross.Plugins.Messenger;
namespace Feedback.Core.Messages
{
    public class FeedbackSavedMessage : MvxMessage
    {
        public FeedbackSavedMessage(object sender)
            : base (sender)
        {
        }
    }
}

using System;
using MvvmCross.Plugins.Messenger;
using Feedback.API.Entities;
namespace Feedback.Core.Messages
{
    public class UserChangedMessage : MvxMessage
    {
        public User CurrentUser { get; private set; }

        public UserChangedMessage(object sender, User currentUser)
        : base (sender)
        {
            CurrentUser = currentUser;
        }
    }
}

using System;
using MvvmCross.Plugins.Messenger;
namespace Feedback.Core.Messages
{
    public class BeaconFoundMessage : MvxMessage
    {
        public string Id { get; }

        public string Name { get; }

        public BeaconFoundMessage(object sender, string id, string name) : base(sender)
        {
            Name = name;

            Id = id;
        }
    }
}

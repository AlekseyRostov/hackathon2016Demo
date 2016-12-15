using System;

namespace Feedback.Core.Entities
{
    public sealed class BeaconActivity
    {
        public BeaconModel BeaconModel { get; set; }

        public DateTime CreationDate { get; set; }
    }
}
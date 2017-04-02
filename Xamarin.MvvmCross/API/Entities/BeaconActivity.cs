using System;

namespace Feedback.API.Entities
{
    public sealed class BeaconActivity
    {
        public BeaconModel BeaconModel { get; set; }

        public DateTime CreationDate { get; set; }
    }
}
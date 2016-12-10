using System;

namespace Feedback.Core.Entities
{
    public class Place : BaseEntity
    {
        public Guid BeaconUUID { get; set; }

        public int BeaconMajor { get; set; }

        public int BeaconMinor { get; set; }
    }
}
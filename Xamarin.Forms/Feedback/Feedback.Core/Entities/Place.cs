using System;
using Microsoft.WindowsAzure.MobileServices;

namespace Feedback.Core.Entities
{
    [DataTable("PlaceItem")]
    public class Place : BaseEntity
    {
        public Guid BeaconUUID { get; set; }

        public int BeaconMajor { get; set; }

        public int BeaconMinor { get; set; }
    }
}
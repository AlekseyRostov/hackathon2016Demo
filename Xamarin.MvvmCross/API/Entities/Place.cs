using System;
using Microsoft.WindowsAzure.MobileServices;

namespace Feedback.API.Entities
{
    [DataTable("PlaceItem")]
    public class Place : BaseEntity
    {
        public string Name { get; set; }
        public Guid BeaconUUID { get; set; }
        public int BeaconMajor { get; set; }
        public int BeaconMinor { get; set; }
    }
}
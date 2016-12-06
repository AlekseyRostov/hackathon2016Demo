
using Microsoft.Azure.Mobile.Server;
using System;

namespace Feedbacks.Azure.DataObjects
{
    /// <summary>
    /// Модель места
    /// </summary>
    public class PlaceItem : EntityData
    {
        public Guid BeaconUUID { get; set; }

        public int BeaconMajor { get; set; }

        public int BeaconMinor { get; set; }
    }
}

using Microsoft.Azure.Mobile.Server;
using System;

namespace Feedbacks.Azure.DataObjects
{
    /// <summary>
    /// Модель места
    /// </summary>
    public class PlaceItem : EntityData
    {
        /// <summary>
        /// Название места
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Характеристика маяка UUID
        /// </summary>
        public Guid BeaconUUID { get; set; }

        /// <summary>
        /// Характеристика маяка Major
        /// </summary>
        public int BeaconMajor { get; set; }

        /// <summary>
        /// Характеристика маяка Minor
        /// </summary>
        public int BeaconMinor { get; set; }
    }
}
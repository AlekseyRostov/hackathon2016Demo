using System;

namespace Feedback.Core.Entities
{
    public class BeaconModel
    {
        public string ID { get; set; } = Guid.NewGuid().ToString();
        public string UUID { get; set; }

        public ushort Major { get; set; }

        public ushort Minor { get; set; }

        public bool Equals(BeaconModel beaconModel)
        {
            return beaconModel.UUID.Equals(UUID) && beaconModel.Major == Major && beaconModel.Minor == Minor;
        }

        public override string ToString()
        {
            return $"[BeaconModel: UUID={UUID}, Major={Major}, Minor={Minor}]";
        }
    }
}
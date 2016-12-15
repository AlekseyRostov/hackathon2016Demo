using System;
using System.Collections.Generic;
using AltBeaconOrg.BoundBeacon;
using Feedback.Core.Entities;
using Feedback.UI.Services.Implementation;

namespace Feedback.UI.Droid.Services
{
    public class BeaconLocationService : BaseBeaconLocationService
    {
        protected override void StartMonitoringInternal(IList<BeaconModel> beaconList)
        {
            MainActivity.Instance.BeaconFound += FoundBeacon;
            MainActivity.Instance.StartMonitoring(beaconList);
        }

        protected override void StopMonitoringInternal()
        {
            MainActivity.Instance.BeaconFound -= FoundBeacon;
            MainActivity.Instance.StopMonitoring();
        }

        private void FoundBeacon(object sender, Beacon beacon)
        {
            MainActivity.Instance.RunOnUiThread(() =>
                                                {
                                                    HandleFoundBeacon(beacon.Id1.ToString(),
                                                                      (ushort) Convert.ToInt32(beacon.Id2.ToString()),
                                                                      (ushort) Convert.ToInt32(beacon.Id3.ToString()));
                                                });
        }
    }
}
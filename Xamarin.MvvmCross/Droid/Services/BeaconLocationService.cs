using System;
using System.Collections.Generic;
using AltBeaconOrg.BoundBeacon;
using Feedback.API.Entities;
using Feedback.Core.Services.Implementations;
using Android.OS;

namespace Feedback.Droid.Services
{
    public class BeaconLocationService : BaseBeaconLocationService
    {
        protected override void StartMonitoringInternal(IList<BeaconModel> beaconList)
        {
            MainApplication.Instance.BeaconFound += FoundBeacon;
            MainApplication.Instance.StartMonitoring(beaconList);
        }

        protected override void StopMonitoringInternal()
        {
            MainApplication.Instance.BeaconFound -= FoundBeacon;
            MainApplication.Instance.StopMonitoring();
        }

        private void FoundBeacon(object sender, Beacon beacon)
        {
            new Android.OS.Handler(Looper.MainLooper).Post(() =>
            {
                HandleFoundBeacon(
                    beacon.Id1.ToString(), 
                    (ushort)Convert.ToInt32(beacon.Id2.ToString()), 
                    (ushort)Convert.ToInt32(beacon.Id3.ToString())
                );
            });
        }
    }
}
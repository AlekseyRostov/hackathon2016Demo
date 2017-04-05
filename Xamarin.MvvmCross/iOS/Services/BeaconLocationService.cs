using System.Collections.Generic;
using System.Diagnostics;
using CoreLocation;
using Feedback.API.Entities;
using Feedback.Core.Services.Implementations;
using Foundation;

namespace Feedback.iOS.Services
{
    public class BeaconLocationService : BaseBeaconLocationService
    {
        private readonly List<CLBeaconRegion> _beaconRegions = new List<CLBeaconRegion>();
        private readonly CLLocationManager _locationManager;

        public BeaconLocationService()
        {
            _locationManager = new CLLocationManager();
        }

        protected override void StartMonitoringInternal(IList<BeaconModel> beaconList)
        {
            _locationManager.DidRangeBeacons += LocationManager_DidRangeBeacons;
            _locationManager.PausesLocationUpdatesAutomatically = false;
            _locationManager.StartUpdatingLocation();
            _locationManager.RequestAlwaysAuthorization();

            foreach(var beacon in beaconList)
            {
                var clBeaconRegion = new CLBeaconRegion(new NSUuid(beacon.UUID), beacon.Major, beacon.Minor, beacon.ID)
                                     {
                                         NotifyEntryStateOnDisplay = true,
                                         NotifyOnEntry = true,
                                         NotifyOnExit = true
                                     };
                _beaconRegions.Add(clBeaconRegion);
                _locationManager.StartMonitoring(clBeaconRegion);
                _locationManager.StartRangingBeacons(clBeaconRegion);

                Debug.WriteLine($"StartMonitoring {beacon.UUID}");
            }
        }

        protected override void StopMonitoringInternal()
        {
            _locationManager.DidRangeBeacons -= LocationManager_DidRangeBeacons;
            _locationManager.StopUpdatingLocation();
            foreach(var region in _beaconRegions)
            {
                _locationManager.StopMonitoring(region);
                _locationManager.StopRangingBeacons(region);
            }
        }

        private void LocationManager_DidRangeBeacons(object sender, CLRegionBeaconsRangedEventArgs e)
        {
            foreach(var beacon in e.Beacons)
            {
                FoundBeacon(beacon);
            }
        }

        private void FoundBeacon(CLBeacon beacon)
        {
            HandleFoundBeacon(beacon.ProximityUuid.AsString(), (ushort) beacon.Major, (ushort) beacon.Minor);
        }
    }
}
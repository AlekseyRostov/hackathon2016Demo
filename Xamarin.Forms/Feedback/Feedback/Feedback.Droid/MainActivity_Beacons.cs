using System;
using System.Collections.Generic;
using System.Linq;
using AltBeaconOrg.BoundBeacon;
using Feedback.Core.Entities;

namespace Feedback.UI.Droid
{
    public partial class MainActivity : IBeaconConsumer
    {
        #region Beacon

        private const int BeaconsUpdatesInSeconds = 5;
        private const long BeaconsUpdatesInMilliseconds = BeaconsUpdatesInSeconds*1000;

        private Region _rangingRegion;

        private RangeNotifier _rangeNotifier;

        private IList<BeaconModel> _listOfBeacons;
        private BeaconManager _beaconManager;

        public event EventHandler<Beacon> BeaconFound;

        public void StartMonitoring(IList<BeaconModel> beacons)
        {
            _beaconManager = BeaconManager.GetInstanceForApplication(this);

            _rangeNotifier = new RangeNotifier();
            _listOfBeacons = beacons;

            //iBeacon
            _beaconManager.BeaconParsers.Add(new BeaconParser().SetBeaconLayout("m:2-3=0215,i:4-19,i:20-21,i:22-23,p:24-24"));

            _beaconManager.Bind(this);
        }

        public void StopMonitoring()
        {
            _beaconManager.StopRangingBeaconsInRegion(_rangingRegion);
            _beaconManager.Unbind(this);
        }

        private void RangingBeaconsInRegion(object sender, ICollection<Beacon> beacons)
        {
            if(beacons != null && beacons.Count > 0)
            {
                var foundBeacons = beacons.ToList();
                foreach(var beacon in beacons)
                {
                    BeaconFound?.Invoke(this, beacon);
                }
            }
        }

        public class RangeNotifier : Java.Lang.Object, IRangeNotifier
        {
            public event EventHandler<ICollection<Beacon>> DidRangeBeaconsInRegionComplete;

            public void DidRangeBeaconsInRegion(ICollection<Beacon> beacons, Region region)
            {
                DidRangeBeaconsInRegionComplete?.Invoke(this, beacons);
            }
        }

        #endregion

        #region IBeaconConsumer implementation

        public void OnBeaconServiceConnect()
        {
            _beaconManager.SetForegroundScanPeriod(BeaconsUpdatesInMilliseconds);
            _beaconManager.SetForegroundBetweenScanPeriod(BeaconsUpdatesInMilliseconds);

            _beaconManager.SetBackgroundScanPeriod(BeaconsUpdatesInMilliseconds);
            _beaconManager.SetBackgroundBetweenScanPeriod(BeaconsUpdatesInMilliseconds);

            _beaconManager.UpdateScanPeriods();

            _rangeNotifier.DidRangeBeaconsInRegionComplete += RangingBeaconsInRegion;
            _beaconManager.SetRangeNotifier(_rangeNotifier);

            _rangingRegion = new Region("region_uid", null, null, null);
            _beaconManager.StartRangingBeaconsInRegion(_rangingRegion);
        }

        #endregion
    }
}
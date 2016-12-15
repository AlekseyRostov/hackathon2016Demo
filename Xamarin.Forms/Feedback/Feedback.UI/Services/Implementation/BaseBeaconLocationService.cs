using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Feedback.Core.Entities;

namespace Feedback.UI.Services.Implementation
{
    public abstract class BaseBeaconLocationService : IBeaconLocationService
    {
        private readonly List<BeaconActivity> _beaconsActivity = new List<BeaconActivity>();
        private bool _isStarted;

#if DEBUG
        private readonly TimeSpan _threshold = TimeSpan.FromMinutes(1);
#else
		private readonly TimeSpan _threshold = TimeSpan.FromMinutes(20);
#endif

        public virtual void StartMonitoring(IEnumerable<BeaconModel> beacons)
        {
            if(_isStarted)
                return;

            _isStarted = true;

            var beaconList = beacons as IList<BeaconModel> ?? beacons.ToList();
            if(beaconList?.Any() != true)
                return;

            StartMonitoringInternal(beaconList);
        }

        public void StopMonitoring()
        {
            _isStarted = false;
            StopMonitoringInternal();
        }

        public event EventHandler<BeaconModel> BeaconFound;

        protected abstract void StartMonitoringInternal(IList<BeaconModel> beaconList);
        protected abstract void StopMonitoringInternal();

        protected void HandleFoundBeacon(string beaconId, ushort major, ushort minor)
        {
            var beaconModel = new BeaconModel
                              {
                                  UUID = beaconId,
                                  Major = major,
                                  Minor = minor
                              };

            var activity = new BeaconActivity
                           {
                               BeaconModel = beaconModel,
                               CreationDate = DateTime.Now
                           };

            //If we registered a beacon in lesser period than threshold we should ignore this event
            var lastActivity = _beaconsActivity.LastOrDefault(x => x.BeaconModel.Equals(activity.BeaconModel) && DateTime.Now - x.CreationDate < _threshold);
            if(lastActivity != null)
            {
                return;
            }

            _beaconsActivity.RemoveAll(s => s.BeaconModel.Equals(activity.BeaconModel));
            _beaconsActivity.Add(activity);

            Debug.WriteLine("Found beacon: {0}.{1} - {2}", major, minor, beaconId);

            BeaconFound?.Invoke(null, beaconModel);
        }
    }
}
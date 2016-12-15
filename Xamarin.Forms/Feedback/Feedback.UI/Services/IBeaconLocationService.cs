using System;
using System.Collections.Generic;
using Feedback.Core.Entities;

namespace Feedback.UI.Services
{
    public interface IBeaconLocationService
    {
        void StartMonitoring(IEnumerable<BeaconModel> beacons);
        void StopMonitoring();
        event EventHandler<BeaconModel> BeaconFound;
    }
}
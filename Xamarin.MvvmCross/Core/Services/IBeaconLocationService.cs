using System;
using System.Collections.Generic;
using Feedback.API.Entities;

namespace Feedback.Core.Services
{
    public interface IBeaconLocationService
    {
        void StartMonitoring(IEnumerable<BeaconModel> beacons);
        void StopMonitoring();
        event EventHandler<BeaconModel> BeaconFound;
    }
}
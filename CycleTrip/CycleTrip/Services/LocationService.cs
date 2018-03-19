using MvvmCross.Platform;
using MvvmCross.Plugins.Location;
using MvvmCross.Plugins.Messenger;
using CycleTrip.Messages;
using System;

namespace CycleTrip.Services
{
    public interface ILocationService
    {
    }

    public class LocationService
        : ILocationService
    {
        private readonly IMvxLocationWatcher _watcher;
        private readonly IMvxMessenger _messenger;

        public LocationService(IMvxLocationWatcher watcher, IMvxMessenger messenger)
        {
            _watcher = watcher;
            _messenger = messenger;
   
            var options = new MvxLocationOptions
            {
                Accuracy = MvxLocationAccuracy.Fine,
                TimeBetweenUpdates = TimeSpan.FromSeconds(2),
                TrackingMode = MvxLocationTrackingMode.Foreground
            };

            _watcher.Start(options, OnLocation, OnError);
        }

        private void OnLocation(MvxGeoLocation location)
        {
            var message = new LocationMessage(this,
                                location.Coordinates.Latitude,
                                location.Coordinates.Longitude,
                                alt: location.Coordinates.Altitude,
                                altacc: location.Coordinates.AltitudeAccuracy,
                                hdg: location.Coordinates.Heading,
                                hdgacc: location.Coordinates.HeadingAccuracy,
                                spd: location.Coordinates.Speed);
            _messenger.Publish(message);
        }

        private void OnError(MvxLocationError error)
        {
            Mvx.Error("Location error: {0}", error.Code);
        }
    }
}

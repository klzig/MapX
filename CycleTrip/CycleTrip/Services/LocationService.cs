using MvvmCross.Platform;
using MvvmCross.Plugins.Location;
using MvvmCross.Plugins.Messenger;
using CycleTrip.Messages;
using System;

// To enable location services for Android api level 23+, runtime permissions are required in 
// addition to the ACCESS_FINE_LOCATION manifest permission.
// https://developer.android.com/training/permissions/requesting.html

namespace CycleTrip.Services
{
    public interface ILocationService
    {
    }

    public class LocationService
        : ILocationService
    {
        private readonly LocationMessage _locationMessage;
        private readonly IMvxLocationWatcher _watcher;
        private readonly IMvxMessenger _messenger;
        private readonly MvxSubscriptionToken _updateLoc_token;

        public LocationService(IMvxLocationWatcher watcher, IMvxMessenger messenger)
        {
            _watcher = watcher;
            _messenger = messenger;
            _locationMessage = new LocationMessage(this);
            _updateLoc_token = _messenger.Subscribe<UpdateLocMessage>(OnUpdateLocMessage);

            var options = new MvxLocationOptions
            {
                Accuracy = MvxLocationAccuracy.Fine,
                TimeBetweenUpdates = TimeSpan.FromSeconds(2),
                TrackingMode = MvxLocationTrackingMode.Foreground
            };

            _watcher.Start(options, OnLocation, OnError);
        }

        private void OnUpdateLocMessage(MvxMessage obj)
        {
            _messenger.Publish(_locationMessage);
        }

        private void OnLocation(MvxGeoLocation location)
        {
            _locationMessage.Update(location.Coordinates.Latitude,
                                    location.Coordinates.Longitude,
                                    acc:location.Coordinates.Accuracy,
                                    alt: location.Coordinates.Altitude,
                                    altacc: location.Coordinates.AltitudeAccuracy,
                                    hdg: location.Coordinates.Heading,
                                    hdgacc: location.Coordinates.HeadingAccuracy,
                                    spd: location.Coordinates.Speed);
            _messenger.Publish(_locationMessage);
        }

        private void OnError(MvxLocationError error)
        {
           _locationMessage.UpdateError(String.Format("{0}", error.Code));
        }
    }
}

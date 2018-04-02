using MvvmCross.Platform;
using MvvmCross.Plugins.Location;
using MvvmCross.Plugins.Messenger;
using CycleTrip.Messages;
using System;
using CycleTrip.Localization;
using System.Threading.Tasks;

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
        private DateTime _timestamp = DateTime.Now;
        private bool _updateTimerStarted = false;

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

        private void OnUpdateLocMessage(MvxMessage obj=null)
        {
            //Mvx.Trace("OnUpdateMessage");
            _locationMessage.Update(Updated());
            _messenger.Publish(_locationMessage);
        }

        private void OnLocation(MvxGeoLocation location)
        {
            //Mvx.Trace("Location Update");
            _timestamp = DateTime.Now;
            _locationMessage.Update(location.Coordinates.Latitude,
                                    location.Coordinates.Longitude,
                                    AppStrings.Now,
                                    acc:location.Coordinates.Accuracy,
                                    alt: location.Coordinates.Altitude,
                                    altacc: location.Coordinates.AltitudeAccuracy,
                                    hdg: location.Coordinates.Heading,
                                    hdgacc: location.Coordinates.HeadingAccuracy,
                                    spd: location.Coordinates.Speed);
            _messenger.Publish(_locationMessage);
            _messenger.Publish(new AlertMessage(this, AlertType.location, false));
            StartUpdateTimer();
        }

        private void OnError(MvxLocationError error)
        {
           _locationMessage.UpdateError(String.Format("{0}", error.Code));
            StartUpdateTimer();
        }

        private string Updated()
        {
            // Show time elapsed since last update
            string updated = "";
            TimeSpan elapsed = DateTime.Now - _timestamp;
            if (elapsed.TotalSeconds > 4)
            {
                updated = elapsed.ToString(@"hh\:mm\:ss") + " " + AppStrings.Ago;
                _messenger.Publish(new AlertMessage(this, AlertType.location, true));
            }
            else
            {
                updated = AppStrings.Now;
                _messenger.Publish(new AlertMessage(this, AlertType.location, false));
            }
            return updated;
        }

        private void StartUpdateTimer()
        {
            if (!_updateTimerStarted)
            {
                _updateTimerStarted = true;
                Task.Run(
                async () =>
                {
                    while (true)
                    {
                        await Task.Delay(4000);
                        OnUpdateLocMessage();
                    }
                });
            }
        }
    }
}

using System;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using CTForms.Models;

namespace CTForms.Services
{
    public interface ILocationService
    {
        event EventHandler Alert;
        event EventHandler LocationUpdate;
    }

    public class AlertEventArgs : EventArgs
    {
        public AlertEventArgs(bool alert)
        {
            Alert = alert;
        }
        public bool Alert { get; }
    }

    public class LocationEventArgs : EventArgs
    {
        public LocationEventArgs(LocationModel location)
        {
            Loc = location;
        }
        public LocationModel Loc { get; }
    }

    public class LocationService : ILocationService
    {
        public event EventHandler Alert;
        public event EventHandler LocationUpdate;
        private LocationModel _location;

        // Singleton
        private static readonly Lazy<LocationService> lazy = new Lazy<LocationService>(() => new LocationService());
        public static LocationService Instance { get { return lazy.Value; } }

        private LocationService()
        {
             _location = new LocationModel();
            Device.StartTimer(new TimeSpan(0, 0, 2), BeginListening);
            BeginListening();
        }

        private bool BeginListening()
        {
            if (LocationUpdate != null)
            {
                try
                {
                    var request = new GeolocationRequest(GeolocationAccuracy.High, new TimeSpan(0, 0, 2));

                    // This can't be called from a background thread, bug https://github.com/xamarin/Essentials/issues/634
                    var task = Geolocation.GetLocationAsync(request);
                    task.ContinueWith(x =>
                    {
                        try
                        {
                            if (x.IsCompleted)
                            {
                                var location = x.Result;
                                if (location == null)
                                {
                                    _location.Update("Timed out getting location");
                                    Alert?.Invoke(this, new AlertEventArgs(true));
                                }
                                else
                                {
                                    _location.Update(location.Latitude,
                                                    location.Longitude,
                                                    location.Accuracy,
                                                    location.Altitude,
                                                    location.Speed,
                                                    location.Course,
                                                    location.Timestamp);
                                    Alert?.Invoke(this, new AlertEventArgs(false));
                                }
                            }
                        }
                        catch {
                            _location.Update("Unknown error");
                            Alert?.Invoke(this, new AlertEventArgs(true));
                        }
                    }, TaskScheduler.FromCurrentSynchronizationContext());
                }
                catch (FeatureNotSupportedException e)
                {
                    // Handle not supported on device exception
                    _location.Update(string.Format("{0}", e.Message));
                    Alert?.Invoke(this, new AlertEventArgs(true));
                }
                catch (PermissionException e)
                {
                    // Handle permission exception
                    _location.Update(string.Format("{0}", e.Message));
                    Alert?.Invoke(this, new AlertEventArgs(true));
                }
                catch (Exception e)
                {
                    // Unable to get location
                    _location.Update(string.Format("{0}", e.Message));
                    Alert?.Invoke(this, new AlertEventArgs(true));
                }

                LocationUpdate?.Invoke(this, new LocationEventArgs(_location));
            }
            return true;
        }
    }
}


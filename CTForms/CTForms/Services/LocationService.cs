using System;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using CTForms.Models;

namespace CTForms.Services
{
    public interface ILocationService
    {
        event EventHandler LocationUpdate;
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
        public event EventHandler LocationUpdate;
        private LocationModel _location;

        public LocationService()
        {
             _location = new LocationModel();
            Device.StartTimer(new TimeSpan(0, 0, 2), BeginListening);
            BeginListening();
        }

        public bool BeginListening()
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
                                }
                                else
                                {
                                    Console.WriteLine($"Latitude: {location.Latitude}, Longitude: {location.Longitude}, Altitude: {location.Altitude}");
                                    _location.Update(location.Latitude,
                                                    location.Longitude,
                                                    location.Accuracy,
                                                    location.Altitude,
                                                    location.Speed,
                                                    location.Course,
                                                    location.Timestamp);
                                }
                            }
                        }
                        catch {

                        }
                    }, TaskScheduler.FromCurrentSynchronizationContext());
                }
                catch (FeatureNotSupportedException e)
                {
                    // Handle not supported on device exception
                    _location.Update(string.Format("{0}", e.Message));
                }
                catch (PermissionException e)
                {
                    // Handle permission exception
                    _location.Update(string.Format("{0}", e.Message));
                }
                catch (Exception e)
                {
                    // Unable to get location
                    _location.Update(string.Format("{0}", e.Message));
                }

                LocationUpdate?.Invoke(this, new LocationEventArgs(_location));
            }
            return true;
        }
    }
}


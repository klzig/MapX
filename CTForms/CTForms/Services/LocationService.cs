using System;
using System.Threading.Tasks;
using CTForms.Models;
using Xamarin.Essentials;

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
        private bool _updateTimerStarted = false;

        public LocationService()
        {
             _location = new LocationModel();
            BeginListening();
        }

        public void BeginListening()
        {
            if (!_updateTimerStarted)
            {
                _updateTimerStarted = true;
                Task.Run(
                async () =>
                {
                    while (_updateTimerStarted)
                    {
                        if (LocationUpdate != null)
                        {
                            try
                            {
                                var request = new GeolocationRequest(GeolocationAccuracy.High, new TimeSpan(0, 0, 4));

                                // This can't be called from a background thread, bug https://github.com/xamarin/Essentials/issues/634
                                var location = await Geolocation.GetLocationAsync(request);

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
                        await Task.Delay(2000);
                    }
                });
            }
        }
    }
}

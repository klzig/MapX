using System;
using CTForms.Models;
using CTForms.Properties;
using CTForms.Services;

namespace CTForms.ViewModels
{
    public class LocationViewModel : BaseViewModel
    {
        public LocationService Loc;

        public LocationViewModel()
        {
            Title = Resources.MenuItemLocation;

            Loc = LocationService.Instance;
            Loc.LocationUpdate += LocationUpdate;
        }

        private void LocationUpdate(object sender, EventArgs e)
        {
            var args = e as LocationEventArgs;
            Location = args.Loc;
        }

        private LocationModel location;
        public LocationModel Location
        {
            get { return location; }
            set { SetProperty(ref location, value); }
        }
    }
}

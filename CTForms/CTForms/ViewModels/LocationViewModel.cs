using CTForms.Models;
using CTForms.Services;

namespace CTForms.ViewModels
{
    public class LocationViewModel : BaseViewModel
    {
        public LocationViewModel()
        {
            Loc.LocationUpdate += LocationUpdate;
        }

        private void LocationUpdate(object sender, System.EventArgs e)
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

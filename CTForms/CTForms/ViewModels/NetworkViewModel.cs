using CTForms.Models;
using CTForms.Properties;
using CTForms.Services;
using System.Linq;
using Xamarin.Essentials;

namespace CTForms.ViewModels
{
    public class NetworkViewModel : BaseViewModel
    {
        public NetworkService Net;

        public NetworkViewModel()
        {
            Title = Resources.Network;

            Net = NetworkService.Instance;
  
            Net.Alert += NetworkAlert;
            Net.TriggerAlert();
        }

        private void NetworkAlert(object sender=null, System.EventArgs e=null)
        {
            Access = TranslateAccess(Net.Network.Access);
            Profiles = TranslateProfiles(Net.Network.Profiles);
        }

        string TranslateAccess(NetworkAccess access)
        {
            switch (access)
            {
                case NetworkAccess.Internet:
                    return Resources.Internet;
                case NetworkAccess.ConstrainedInternet:
                    return Resources.ConstrainedInternet;
                case NetworkAccess.Local:
                    return Resources.Local;
                case NetworkAccess.None:
                    return Resources.None;
            }
            return Resources.Unknown;
        }

        string TranslateProfiles(System.Collections.Generic.IEnumerable<ConnectionProfile> profiles)
        {
            string p = "";
            if (profiles.Contains(ConnectionProfile.Ethernet))
            {
                p += Resources.Ethernet;
            }
            if (profiles.Contains(ConnectionProfile.WiFi))
            {
                if (p != "")
                    p += ", ";
                p += "WiFi";
            }
            if (profiles.Contains(ConnectionProfile.Cellular))
            {
                if (p != "")
                    p += ", ";
                p += Resources.Cellular;
            }
            if (profiles.Contains(ConnectionProfile.Unknown))
            {
                if (p != "")
                    p += ", ";
                p += Resources.Unknown;
            }
            if (p == "")
                p = p += Resources.Unknown + ", " + Resources.IsAirplaneMode;
            return p;
        }

        private string access;
        public string Access
        {
            get { return access; }
            set { SetProperty(ref access, value); }
        }

        private string profiles;
        public string Profiles
        {
            get { return profiles; }
            set { SetProperty(ref profiles, value); }
        }
    }
}

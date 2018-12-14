using System;
using System.Linq;
using Xamarin.Essentials;
using CTForms.Models;

namespace CTForms.Services
{
    public interface INetworkService
    {
        event EventHandler Alert;
        void TriggerAlert();
    }

    public class NetworkAlertEventArgs : EventArgs
    {
        public NetworkAlertEventArgs(bool alert)
        {
            Alert = alert;
        }
        public bool Alert { get; }
    }

    public class NetworkService : INetworkService
    {
        public event EventHandler Alert;
        public NetworkModel Network { get; set; } = new NetworkModel();

        // Singleton
        private static readonly Lazy<NetworkService> lazy = new Lazy<NetworkService>(() => new NetworkService());
        public static NetworkService Instance { get { return lazy.Value; } }

        private NetworkService()
        {
            // Initial state
            TriggerAlert();

            Connectivity.ConnectivityChanged += ConnectivityChanged;
        }

        public void TriggerAlert()
        {
            Network.Access = Connectivity.NetworkAccess;
            Network.Profiles = Connectivity.ConnectionProfiles;
            SendAlert();
        }

        void ConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
        {
            Network.Access = e.NetworkAccess;
            Network.Profiles = e.ConnectionProfiles;
            SendAlert();
        }

        void SendAlert() {
            if (Network.Access == NetworkAccess.Internet)
                Alert?.Invoke(this, new NetworkAlertEventArgs(false));
            else
                Alert?.Invoke(this, new NetworkAlertEventArgs(true));
        }
    }
}


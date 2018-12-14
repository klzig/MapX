using System.Collections.Generic;
using Xamarin.Forms;
using CTForms.Services;

namespace CTForms.ViewsBase
{
    public class CommonToolbarPage : ContentPage
    {
        public LocationService Loc;
        public NetworkService Net;

        private enum Item
        { 
            notification,
            location,
            network
        }

        private readonly List<Item> ItemOrder = new List<Item>()
        {   // Always show toolbar items in same order
            // Left side of toolbar
            Item.location,
            Item.network,
            Item.notification
            // Right side of toolbar
        };

        private readonly Dictionary<Item, ToolbarItem> AllToolbarItems = new Dictionary<Item, ToolbarItem>()
        {
            { Item.notification,
              new ToolbarItem("Action Name", "notifications", () =>
                {   // On clicked
                    MessagingCenter.Send("notifications", "NotificationAlertClicked");
                }
                ) { Text = Properties.Resources.Notifications, Priority = 0, Order = ToolbarItemOrder.Primary }},
   
            { Item.location,
              new ToolbarItem("Action Name", "location", () =>
                {   // On clicked
                    MessagingCenter.Send("location", "LocationAlertClicked");
                }
                ) { Text = Properties.Resources.Location, Priority = 0, Order = ToolbarItemOrder.Primary }},

            { Item.network,
              new ToolbarItem("Action Name", "network", () =>
                {   // On clicked
                    MessagingCenter.Send("network", "NetworkAlertClicked");
                }
                ) { Text = Properties.Resources.Network, Priority = 0, Order = ToolbarItemOrder.Primary }}
        };

        private Dictionary<Item, bool> ItemsEnabled = new Dictionary<Item, bool>()
        {
            {Item.location, false },
            {Item.notification, true },
            {Item.network, false }
        };

        public CommonToolbarPage() : base()
        {
            RefreshToolbar();
            Loc = LocationService.Instance;
            Loc.Alert += LocAlert;
            Net = NetworkService.Instance;
            Net.Alert += NetAlert;
            Net.TriggerAlert();
        }

        private void LocAlert(object sender, System.EventArgs e)
        {
            var args = e as AlertEventArgs;
            UpdateAlert(args.Alert, Item.location);
        }

        private void NetAlert(object sender, System.EventArgs e)
        {
            var args = e as NetworkAlertEventArgs;
            UpdateAlert(args.Alert, Item.network);
        }

        private void UpdateAlert(bool active, Item item)
        {
            ItemsEnabled[item] = active;
            if (active)
                RefreshToolbar();
            else
                ToolbarItems.Remove(AllToolbarItems[item]);
        }

        private void RefreshToolbar()
        {
            ToolbarItems.Clear();
            foreach (Item item in ItemOrder)
                if (ItemsEnabled[item])
                    ToolbarItems.Add(AllToolbarItems[item]);
        }
    }
}
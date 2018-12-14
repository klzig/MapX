using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using CTForms.Models;
using CTForms.ViewsBase;

namespace CTForms.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : MasterDetailPage
    {
        private Dictionary<int, NavigationPage> NavigationPages;
        private Dictionary<int, CommonToolbarPage> Pages; 
        private CommonToolbarPage NotificationsAlertPage;
        private CommonToolbarPage LocationAlertPage;
        private CommonToolbarPage NetworkAlertPage;

        public MainPage()
        {
            InitializeComponent();

            MasterBehavior = MasterBehavior.Popover;
            if (Device.RuntimePlatform == Device.iOS)
            {   // The iOS gesture that opens the main menu interferes too much with gesture-enabled views.
                // Android isn't as bad because the gesture is only active from the left edge.
                // Disabling the gesture for Android also disables the hamburger menu.
                // Android allows the menu to be opened by gesture even on child pages with no hamburger.
                // UWP also has the hamburger on child pages.  Probably not a show-stopper.
                IsGestureEnabled = false;
            }

            if (Device.RuntimePlatform == Device.iOS)
            {
                NavigationPages = new Dictionary<int, NavigationPage>
                {
                    { (int)MenuItemType.Map, new NavigationPage(new MapPage()) },
                    { (int)MenuItemType.Browse, new NavigationPage(new ItemsPage()) },
                    { (int)MenuItemType.About, new NavigationPage(new AboutPage()) },
                    { (int)MenuItemType.Notifications, new NavigationPage(new NotificationsPage()) },
                    { (int)MenuItemType.Location, new NavigationPage(new LocationPage()) }
                };
            }
            else
            {
                Pages = new Dictionary<int, CommonToolbarPage>
                {
                    { (int)MenuItemType.Map, new MapPage() },
                    { (int)MenuItemType.Browse, new ItemsPage() },
                    { (int)MenuItemType.About, new AboutPage() },
                    { (int)MenuItemType.Notifications, new NotificationsPage() },
                    { (int)MenuItemType.Location, new LocationPage() }
                };
            }


            MessagingCenter.Subscribe<string>("notifications", "NotificationAlertClicked", async (sender) =>
            {
                if (NotificationsAlertPage == null)
                {
                    NotificationsAlertPage = new NotificationsAlertPage();
                }
                try
                {
                    await Detail.Navigation.PushAsync(NotificationsAlertPage);
                }
                catch (InvalidOperationException)
                {   // Don't push more than one page onto navigation stack
                }
            
            });

            MessagingCenter.Subscribe<string>("location", "LocationAlertClicked", async (sender) =>
            {
                if (LocationAlertPage == null)
                {
                    LocationAlertPage = new LocationAlertPage();
                }
                try
                {
                    await Detail.Navigation.PushAsync(LocationAlertPage);
                }
                catch (InvalidOperationException)
                {   // Don't push more than one page onto navigation stack
                }
            });

            MessagingCenter.Subscribe<string>("network", "NetworkAlertClicked", async (sender) =>
            {
                if (NetworkAlertPage == null)
                {
                    NetworkAlertPage = new NetworkAlertPage();
                }
                try
                {
                    await Detail.Navigation.PushAsync(NetworkAlertPage);
                }
                catch (InvalidOperationException)
                {   // Don't push more than one page onto navigation stack
                }
            });
        }

        public async Task NavigateFromMenu(int id)
        {
            if (Device.RuntimePlatform == Device.iOS)
            {   // Can't reuse NavigationPage because iOS loses hamburger icon (Xamarin bug).
                // However iOS handles this without flashing the toolbar when navigating the menu.
                var newPage = NavigationPages[id];

                if (newPage != null && Detail != newPage)
                {
                    Detail = newPage;

                    if (Device.RuntimePlatform == Device.Android)
                        await Task.Delay(100);
                }
            }
            else
            {   // Reuse the NavigationPage to avoid flashing the toolbar.
                var oldPage = Detail.Navigation.NavigationStack[0];
                var newPage = Pages[id];

                if (newPage != null && Detail != newPage)
                {
                    try
                    {
                        Detail.Navigation.InsertPageBefore(newPage, oldPage);
                    }
                    catch (ArgumentException)
                    {   // Don't navigate to same root page
                    }
                    await Detail.Navigation.PopToRootAsync();
                }
            }
            IsPresented = false;
        }
    }
}
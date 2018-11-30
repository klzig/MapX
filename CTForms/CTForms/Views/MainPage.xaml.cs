using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using CTForms.Models;

namespace CTForms.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : MasterDetailPage
    {
        Dictionary<int, NavigationPage> MenuPages = new Dictionary<int, NavigationPage>();
  
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

            MenuPages.Add((int)MenuItemType.Map, new NavigationPage(new MapPage()));
            MenuPages.Add((int)MenuItemType.Browse, new NavigationPage(new ItemsPage()));
            MenuPages.Add((int)MenuItemType.About, new NavigationPage(new AboutPage()));
            MenuPages.Add((int)MenuItemType.Notifications, new NavigationPage(new NotificationsPage()));

            MessagingCenter.Subscribe<string>("Notifications", "Clicked", async (sender) =>
            {
                await Detail.Navigation.PushAsync(new NotificationsPage());
            });
        }

        public async Task NavigateFromMenu(int id)
        {
            var newPage = MenuPages[id];

            if (newPage != null && Detail != newPage)
            {
                Detail = newPage;

                if (Device.RuntimePlatform == Device.Android)
                    await Task.Delay(100);
            }

            IsPresented = false;
        }
    }
}
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

            MenuPages.Add((int)MenuItemType.Browse, (NavigationPage)Detail);

            MessagingCenter.Subscribe<string>("Notifications", "Clicked", async (sender) =>
            {
                await Detail.Navigation.PushAsync(new NotificationsPage());
            });
        }

        public async Task NavigateFromMenu(int id)
        {
            if (!MenuPages.ContainsKey(id))
            {
                switch (id)
                {
                    case (int)MenuItemType.Map:
                        MenuPages.Add(id, new NavigationPage(new MapPage()));
                        break;
                    case (int)MenuItemType.Browse:
                        MenuPages.Add(id, new NavigationPage(new ItemsPage()));
                        break;
                    case (int)MenuItemType.About:
                        MenuPages.Add(id, new NavigationPage(new AboutPage()));
                        break;
                    case (int)MenuItemType.Notifications:
                        MenuPages.Add(id, new NavigationPage(new NotificationsPage()));
                        break;
                }
            }

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
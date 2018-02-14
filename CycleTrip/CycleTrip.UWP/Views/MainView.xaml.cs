using System.Collections.Generic;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using MvvmCross.Plugins.Messenger;
using MvvmCross.Platform;
using CycleTrip.ViewModels;
using CycleTrip.Messages;

// https://docs.microsoft.com/en-us/windows/uwp/design/shell/title-bar
// https://stackoverflow.com/questions/47823501/how-to-set-the-foreground-color-of-text-in-navigationview

namespace CycleTrip.UWP.Views
{
    /// <summary>
    /// The navigation shell, common to all pages.  It managages the top CommandBar, NavigationView, and hosts views in ContentFrame. 
    /// </summary>
    public sealed partial class MainView : MainViewBase
    {
        Dictionary<AlertType, AppBarButton> _alerts = new Dictionary<AlertType, AppBarButton> {
            {AlertType.notification, null},
            {AlertType.recording, null }
        };

        private readonly IMvxMessenger _messenger;
        private readonly MvxSubscriptionToken _alert_token;
        private readonly MvxSubscriptionToken _title_token;
 
        public MainView()
        {
            InitializeComponent();

            _messenger = Mvx.Resolve<IMvxMessenger>();
            _alert_token = _messenger.Subscribe<AlertMessage>(OnAlertMessage);
            _title_token = _messenger.Subscribe<ViewTitleMessage>(OnViewTitleMessage);
        }

        private void OnAlertMessage(AlertMessage alert)
        {
            // Hack around a race with SelfTest
            if (_alerts[alert.Type] == null)
            {
                return;
            }

            if (alert.Visible)
            {
      //          SolidColorBrush brush = new SolidColorBrush(Color.FromArgb(0xff, 0xe1, 0x03, 0x32));
      //          _alerts[alert.Type].Foreground = brush;
                _alerts[alert.Type].Visibility = Visibility.Visible;
            }
            else
            {
      //          SolidColorBrush brush = new SolidColorBrush(Color.FromArgb(0xff, 0xef, 0xf2, 0xf6));
      //          _alerts[alert.Type].Foreground = brush;
                _alerts[alert.Type].Visibility = Visibility.Collapsed;
            }
        }

        private void OnViewTitleMessage(ViewTitleMessage msg)
        {
            var loader = new Windows.ApplicationModel.Resources.ResourceLoader();
            string title = loader.GetString(msg.Title);
            hamburgerMenuControl.Header = title;
        }

        // http://blog.jerrynixon.com/2012/09/how-to-access-named-control-inside-xaml.html
        private void Alert_Loaded(object sender, RoutedEventArgs e)
        {
            AppBarButton btn = sender as AppBarButton;
            switch (btn.Name)
            {
                case "RecordingAlert":
                    _alerts[AlertType.recording] = btn;
                    break;
                case "NotificationsAlert":
                    _alerts[AlertType.notification] = btn;
                    break;
            }
        }

        private void NotificationsAlert_Click(object sender, RoutedEventArgs e)
        {
            int position = 1;
            ViewModel.NavigateTo(position);
        }
 
        private void OnMenuItemClick(NavigationView sender, NavigationViewItemInvokedEventArgs e)
        {
            if (e.IsSettingsInvoked)
            {
                ViewModel.NavigateTo(2);
            }
            else
            {
                var item = e.InvokedItem as MenuItem;
                ViewModel.NavigateTo(item.Index);
            }
        }

        private void MainViewBase_Loaded(object sender, RoutedEventArgs e)
        {
            hamburgerMenuControl.MenuItemsSource = MenuItem.GetMainItems(ViewModel.ModelMenuItems);
            ViewModel.NavigateTo(0);
        }
    }

    /// <summary>
    /// A UWP main menu listview item
    /// </summary>
    public class MenuItem
    {
        public string Icon { get; set; }
        public string Label { get; set; }
        public int Index { get; set; }  // Pass-through to view model because UWP doesn't provide index of selected item

        public static List<MenuItem> GetMainItems(ModelMenuItem[] menuItems)
        {
            var items = new List<MenuItem>();

            items.Add(new MenuItem() { Icon = "\uEC1B", Label = menuItems[0].MenuName, Index = 0 });
            items.Add(new MenuItem() { Icon = "\uEC42", Label = menuItems[1].MenuName, Index = 1 });

            return items;
        }
    }
}

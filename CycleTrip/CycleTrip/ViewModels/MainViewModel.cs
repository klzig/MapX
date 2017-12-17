using System;
using MvvmCross.Core.ViewModels;
using MvvmCross.Core.Navigation;
using CycleTrip.PresentationHints;
using CycleTrip.Messages;
using MvvmCross.Plugins.Messenger;
using System.Threading.Tasks;

namespace CycleTrip.ViewModels
{
    public class MainViewModel : MvxViewModel
    {
        readonly Type[] _menuItemTypes = {
            typeof(FirstPageViewModel),
            typeof(SettingsViewModel),
        };

        public MenuItem[] MenuItems = {
            new MenuItem("First Page", 0),
            new MenuItem("Settings", 0)
        };

        private readonly IMvxNavigationService _navigationService;
        private readonly IMvxMessenger _messenger;

        public MainViewModel(IMvxNavigationService navigationService, IMvxMessenger messenger)
        {
            _navigationService = navigationService;
            _messenger = messenger;
          }

        public async void SelfTest()
        {
            var notification_true = new AlertMessage(this, AlertType.notification, true);
            var notification_false = new AlertMessage(this, AlertType.notification, false);
            var recording_true = new AlertMessage(this, AlertType.recording, true);
            var recording_false = new AlertMessage(this, AlertType.recording, false);
            _messenger.Publish(notification_true);
            _messenger.Publish(recording_true);
            await Task.Delay(2000);
            _messenger.Publish(notification_true);
            _messenger.Publish(recording_false);
        }

        public void ShowDefaultMenuItemAsync()
        {
            NavigateTo(0);
        }

        public void NavigateTo(int position)
        {
            Type vm = _menuItemTypes[position];
            ChangePresentation(new ClearBackstackHint());
//           var presentationBundle = new MvxBundle(new Dictionary<string, string> { { "NavigationMode", "ClearStack" } });
            _navigationService.Navigate(vm, null);  // Fragment's OnCreateView not called unless second parameter is null

        }

  //      public async Task NavigateTo(int position)
  //      {
  //          Type vm = _menuItemTypes[position];
  //          var presentationBundle = new MvxBundle(new Dictionary<string, string> { {"NavigationMode", "ClearStack"} });
  //          await _navigationService.Navigate(vm, null);
  //      }

        public IMvxCommand ResetTextCommand => new MvxCommand(ResetText);
        private void ResetText()
        {
            Text = "Hello MvvmCross";
        }

        private string _text = "Hello MvvmCross";
        public string Text
        {
            get { return _text; }
            set { SetProperty(ref _text, value); }
        }
    }

    public class MenuItem
    {
        public string MenuName { get; set; }
        public int IconId { get; set; }

        public MenuItem(string menuName, int iconId)
        {
            MenuName = menuName;
            IconId = iconId;
        }
    }
}

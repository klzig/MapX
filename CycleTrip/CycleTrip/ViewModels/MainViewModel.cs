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
        public ModelMenuItem[] ModelMenuItems = {
            new ModelMenuItem("First Page", typeof(FirstPageViewModel)),
            new ModelMenuItem("Information", typeof(InfoViewModel)),
            new ModelMenuItem("Settings", typeof(SettingsViewModel))
        };

        private readonly IMvxNavigationService _navigationService;
        private readonly IMvxMessenger _messenger;

        public MainViewModel(IMvxNavigationService navigationService, IMvxMessenger messenger)
        {
            _navigationService = navigationService;
            _messenger = messenger;
        }

        public override Task Initialize()
        {
            //TODO: Add starting logic here

            return base.Initialize();
        }

        public override void ViewAppeared()
        {
            base.ViewAppeared();
 
            // Show first view after cold start
      //      NavigateTo(0);
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

        public void NavigateTo(int position)
        {
            Type vm = ModelMenuItems[position].ViewType;
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
  
        public IMvxAsyncCommand FirstPageCommand
        {
            get => new MvxAsyncCommand(() => _navigationService.Navigate<FirstPageViewModel>());
        }

        public IMvxCommand SecondPageCommand
        {
            get => new MvxCommand(() => _navigationService.Navigate<SecondPageViewModel>());
        }

        public IMvxCommand InfoCommand
        {
            get => new MvxCommand(() => _navigationService.Navigate<InfoViewModel>());
        }

        public IMvxCommand SettingsCommand
        {
            get => new MvxCommand(() => _navigationService.Navigate<SettingsViewModel>());
        }
    }

    /// <summary>
    /// Common elements of main menu listview item
    /// </summary>
    public class ModelMenuItem
    {
        public string MenuName { get; set; }
        public Type ViewType { get; set; }

        public ModelMenuItem(string name, Type type)
        {
            MenuName = name;
            ViewType = type;
        }
    }
}

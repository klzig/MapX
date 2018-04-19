using System;
using MvvmCross.Core.ViewModels;
using MvvmCross.Core.Navigation;
using CycleTrip.PresentationHints;
using CycleTrip.Messages;
using CycleTrip.Localization;
using MvvmCross.Plugins.Messenger;
using System.Threading.Tasks;
using CycleTrip.Services;

namespace CycleTrip.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public ModelMenuItem[] ModelMenuItems = {
            new ModelMenuItem(AppStrings.FirstPage, typeof(FirstPageViewModel)),
            new ModelMenuItem(AppStrings.Map, typeof(MapViewModel)),
            new ModelMenuItem(AppStrings.Information, typeof(InfoViewModel)),
            new ModelMenuItem(AppStrings.Location, typeof(LocationViewModel)),
            new ModelMenuItem(AppStrings.Settings, typeof(SettingsViewModel))
        };

        private readonly IMvxNavigationService _navigationService;
        private readonly IMvxMessenger _messenger;

        public MainViewModel(IMvxNavigationService navigationService, IMvxMessenger messenger, ILocationService loc)
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
            NavigateToAsync(0);
            SelfTest();
        }

        public async void SelfTest()
        {
            var notification_true = new AlertMessage(this, AlertType.notification, true);
            var notification_false = new AlertMessage(this, AlertType.notification, false);
            _messenger.Publish(notification_true);
            await Task.Delay(2000);
            _messenger.Publish(notification_true);
        }

        public async void NavigateToAsync(int position)
        {
            Type vm = ModelMenuItems[position].ViewType;
            //var presentationBundle = new MvxBundle(new Dictionary<string, string> { { "NavigationMode", "ClearStack" } });
            await _navigationService.Navigate(vm, null);
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

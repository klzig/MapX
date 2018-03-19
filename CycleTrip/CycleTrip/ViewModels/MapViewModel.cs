using CycleTrip.Localization;
using CycleTrip.Messages;
using CycleTrip.Services;
using MvvmCross.Core.Navigation;
using MvvmCross.Plugins.Messenger;

namespace CycleTrip.ViewModels
{
    public class MapViewModel : BaseViewModel
    {
        private readonly IMvxNavigationService _navigationService;
        private readonly IMvxMessenger _messenger;

        public MapViewModel(IMvxNavigationService navigationService, IMvxMessenger messenger, ILocationService loc)
        {
            _navigationService = navigationService;
            _messenger = messenger;
        }

        public override void ViewAppeared()
        {
            var new_title = new ViewTitleMessage(this, AppStrings.Map);
            _messenger.Publish(new_title);
        }
    }
}

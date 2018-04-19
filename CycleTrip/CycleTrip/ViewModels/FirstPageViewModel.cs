using System.Collections.Generic;
using MvvmCross.Core.Navigation;
using MvvmCross.Core.ViewModels;
using MvvmCross.Plugins.Messenger;
using CycleTrip.Localization;
using CycleTrip.Messages;

namespace CycleTrip.ViewModels
{
    public class FirstPageViewModel : BaseViewModel
    {
        private readonly IMvxNavigationService _navigationService;
        private readonly IMvxMessenger _messenger;

        public FirstPageViewModel(IMvxNavigationService navigationService, IMvxMessenger messenger)
        {
            _navigationService = navigationService;
            _messenger = messenger;
        }

        public override void ViewAppeared()
        {
            var new_title = new ViewTitleMessage(this, AppStrings.FirstPage);
            _messenger.Publish(new_title);
        }

        private MvxAsyncCommand _secondPageCommand;
        public IMvxAsyncCommand SecondPageCommand
        {
            get
            {
                _secondPageCommand = _secondPageCommand ?? new MvxAsyncCommand(() => 
                    _navigationService.Navigate<SecondPageViewModel>(new MvxBundle(new Dictionary<string, string> { { "NavigationMode", "Push" } })));
                return _secondPageCommand;
            }
        }
    }
}

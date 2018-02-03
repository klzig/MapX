using CycleTrip.Messages;
using MvvmCross.Core.Navigation;
using MvvmCross.Core.ViewModels;
using MvvmCross.Plugins.Messenger;
using System.Collections.Generic;
using System.Windows.Input;

namespace CycleTrip.ViewModels
{
    public class FirstPageViewModel : MvxViewModel
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
            var new_title = new ViewTitleMessage(this, "first_page");
            _messenger.Publish(new_title);
        }

        public IMvxCommand SecondPageCommand
        {
            get => new MvxCommand(() => _navigationService.Navigate<SecondPageViewModel>(new MvxBundle(new Dictionary<string, string> { { "NavigationMode", "Push" } })));
        }

        private string _ButtonText = "Second Page";
        public string ButtonText
        {
            get => _ButtonText;
        //    set => SetProperty(ref _ButtonText, value);
        }
    }
}

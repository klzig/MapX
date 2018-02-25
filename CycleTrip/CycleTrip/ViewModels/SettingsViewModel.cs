using CycleTrip.Messages;
using MvvmCross.Core.ViewModels;
using MvvmCross.Plugins.Messenger;

namespace CycleTrip.ViewModels
{
    public class SettingsViewModel : BaseViewModel
    {
        private readonly IMvxMessenger _messenger;

        public SettingsViewModel(IMvxMessenger messenger)
        {
            _messenger = messenger;
        }

        public override void ViewAppeared()
        {
            var new_title = new ViewTitleMessage(this, "settings");
            _messenger.Publish(new_title);
        }
    }
}
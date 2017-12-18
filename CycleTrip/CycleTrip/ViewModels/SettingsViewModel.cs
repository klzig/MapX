using CycleTrip.Messages;
using MvvmCross.Core.ViewModels;
using MvvmCross.Plugins.Messenger;

namespace CycleTrip.ViewModels
{
    public class SettingsViewModel : MvxViewModel
    {
        private readonly IMvxMessenger _messenger;

        public SettingsViewModel(IMvxMessenger messenger)
        {
            _messenger = messenger;
        }

        public override void ViewAppearing()
        {
            var new_title = new ViewTitleMessage(this, "Settings");
            _messenger.Publish(new_title);
        }
    }
}
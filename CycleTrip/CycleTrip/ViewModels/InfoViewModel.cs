using CycleTrip.Messages;
using MvvmCross.Core.ViewModels;
using MvvmCross.Plugins.Messenger;

namespace CycleTrip.ViewModels
{
    public class InfoViewModel : MvxViewModel
    {
        private readonly IMvxMessenger _messenger;
    
        public InfoViewModel(IMvxMessenger messenger)
        {
            _messenger = messenger;
        }

        public override void ViewAppeared()
        {
            var new_title = new ViewTitleMessage(this, "info");
            _messenger.Publish(new_title);
        }
    }
}

using CycleTrip.Messages;
using MvvmCross.Core.ViewModels;
using MvvmCross.Plugins.Messenger;

namespace CycleTrip.ViewModels
{
    public class SecondPageViewModel : MvxViewModel
    {
        private readonly IMvxMessenger _messenger;
    
        public SecondPageViewModel(IMvxMessenger messenger)
        {
            _messenger = messenger;
        }

        public override void ViewAppeared()
        {
            var new_title = new ViewTitleMessage(this, "second_page");
            _messenger.Publish(new_title);
        }
    }
}

using CycleTrip.Messages;
using MvvmCross.Core.ViewModels;
using MvvmCross.Plugins.Messenger;
using System.Windows.Input;

namespace CycleTrip.ViewModels
{
    public class FirstPageViewModel : MvxViewModel
    {
        private readonly IMvxMessenger _messenger;

        public FirstPageViewModel(IMvxMessenger messenger)
        {
            _messenger = messenger;
        }

        public override void ViewAppearing()
        {
            var new_title = new ViewTitleMessage(this, "First Page");
            _messenger.Publish(new_title);
        }

        public ICommand MyCommand
        {
            get => new MvxCommand(() => ShowViewModel<SecondPageViewModel>(), () => true);
        }

        private string _ButtonText = "Second Page";
        public string ButtonText
        {
            get => _ButtonText;
        //    set => SetProperty(ref _ButtonText, value);
        }
    }
}

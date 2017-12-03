using MvvmCross.Core.ViewModels;
using System.Windows.Input;

namespace CycleTrip.ViewModels
{
    public class FirstPageViewModel : MvxViewModel
    {  
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

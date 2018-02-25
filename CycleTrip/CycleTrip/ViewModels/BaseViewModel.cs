using MvvmCross.Core.ViewModels;
using MvvmCross.Localization;

namespace CycleTrip.ViewModels
{
    public class BaseViewModel : MvxViewModel
    {
        public IMvxLanguageBinder SharedSource => new MvxLanguageBinder("","");
        public IMvxLanguageBinder TextSource => new MvxLanguageBinder("", GetType().Name);
    }
}


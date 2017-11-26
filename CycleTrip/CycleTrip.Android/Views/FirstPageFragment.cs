using Android.OS;
using Android.Runtime;
using Android.Views;
using CycleTrip.ViewModels;
using MvvmCross.Droid.Support.V4;
using MvvmCross.Droid.Views.Attributes;

namespace CycleTrip.Droid.Views
{
    [MvxFragmentPresentation(typeof(MainViewModel), Resource.Id.frameLayout, true)]
    [Register("CycleTrip.Droid.FirstPageFragment")]
    public class FirstPageFragment : MvxFragment<FirstPageViewModel>
    {
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            return inflater.Inflate(Resource.Layout.FirstPageView, container, false);
        }
    }
}
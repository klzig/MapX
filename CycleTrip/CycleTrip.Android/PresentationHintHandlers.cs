using Android.Support.V4.App;
using MvvmCross.Platform;
using MvvmCross.Platform.Droid.Platform;
using CycleTrip.PresentationHints;

// http://blog.mzikmund.com/2017/03/modifying-uwp-navigation-backstack-with-mvvmcross/#more-670
// https://stackoverflow.com/questions/16157597/what-is-the-best-way-to-handle-goback-for-the-different-mvvmcross-v3-platforms
// https://github.com/MvvmCross/MvvmCross-Samples/blob/master/XPlatformMenus/XPlatformMenus.Droid/Setup.cs

namespace CycleTrip.Droid
{
    /// <summary>
    /// Handles back stack presentation hints
    /// </summary>
    public class BackStackHintHandler
    {
        public bool HandleClearBackstackHint(ClearBackstackHint clearBackstackHint)
        {
            var fragmentActivity = Mvx.Resolve<IMvxAndroidCurrentTopActivity>().Activity as FragmentActivity;
            for (int i = 0; i < fragmentActivity.SupportFragmentManager.BackStackEntryCount; i++)
            {
                fragmentActivity.SupportFragmentManager.PopBackStack();
            }
            return true;
        }
    }
}
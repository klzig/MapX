using Android.App;
using Android.Content.PM;
using MvvmCross.Droid.Views;

namespace CycleTrip.Droid
{
    [Activity(
        Label = "Cycle Trip"
        , MainLauncher = true
        , Icon = "@drawable/ic_splash"
        , Theme = "@style/MyTheme.Splash"
        , NoHistory = true)]
    public class SplashScreen : MvxSplashScreenActivity
    {
        public SplashScreen()
        {
        }
    }
}

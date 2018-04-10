using Android.App;
using MvvmCross.Droid.Views;

namespace CycleTrip.Droid
{
    [Activity(
        Label = "CycleTrip"
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

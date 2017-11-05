using Android.App;
using Android.Content.PM;
using MvvmCross.Droid.Views;

namespace CycleTrip.Droid
{
    [Activity(
        Label = "CycleTrip.Droid"
        , MainLauncher = true
        , Icon = "@mipmap/splash"
        , Theme = "@style/Theme.Splash"
        , NoHistory = true
        , ScreenOrientation = ScreenOrientation.Portrait)]
    public class SplashScreen : MvxSplashScreenActivity
    {
        public SplashScreen()
            : base(Resource.Layout.SplashScreen)
        {
        }
    }
}

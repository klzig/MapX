
using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Views;
using Android.Graphics;
using Com.Mapbox.Mapboxsdk;
using CTForms.Services;

namespace CTForms.Droid
{
    [Activity(Label = "CTForms", Icon = "@mipmap/icon", Theme = "@style/splashscreen", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.Window.RequestFeature(WindowFeatures.ActionBar);
            base.SetTheme(Resource.Style.MainTheme);

            base.OnCreate(savedInstanceState);
            Mapbox.GetInstance(this, Secrets.AndroidMapboxToken);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            Xamarin.Forms.Forms.Init(this, savedInstanceState);

            System.Diagnostics.Debug.WriteLine("Mapbox version: " + BuildConfig.MapboxVersionString);

            LoadApplication(new App());

            // Set status bar background color to match NavigationPage BarBackgroundColor
            Window.ClearFlags(WindowManagerFlags.TranslucentStatus);
            Window.AddFlags(WindowManagerFlags.DrawsSystemBarBackgrounds);
            Window.SetStatusBarColor(Color.Rgb(44, 138, 255));
        }
    }
}
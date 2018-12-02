using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using CTForms.Views;
using CTForms.Services;

// To change theme at runtime
// App.Current.Resources ["BackgroundColor"] = Color.White;
// App.Current.Resources ["TextColor"] = Color.Black;
// etc.

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace CTForms
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            if (Device.RuntimePlatform == Device.iOS || Device.RuntimePlatform == Device.Android)
            {
                var ci = DependencyService.Get<ILocalize>().GetCurrentCultureInfo();
                CTForms.Properties.Resources.Culture = ci; // set the RESX for resource localization
                DependencyService.Get<ILocalize>().SetLocale(ci); // set the Thread for locale-aware methods
            }

            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }

    }
}

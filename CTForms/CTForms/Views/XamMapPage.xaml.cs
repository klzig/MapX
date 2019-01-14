using System;

using Xamarin.Forms.Xaml;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

using CTForms.ViewsBase;
using CTForms.ViewModels;
using Xamarin.Forms.Maps;

// https://docs.microsoft.com/en-us/xamarin/xamarin-forms/user-interface/map

namespace CTForms.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class XamMapPage : CommonToolbarPage
    {
        public XamMapPage()
        {
            On<iOS>().SetUseSafeArea(true);

            InitializeComponent();

            location.Clicked += OnClick;

        }

        public void OnClick(object sender, EventArgs e)
        {
            var span = MapSpan.FromCenterAndRadius(
                   new Position(((MapViewModel)BindingContext).Location.Lat, ((MapViewModel)BindingContext).Location.Lon), Distance.FromMiles(0.3));
            map.MoveToRegion(span);
        }   
    }
}

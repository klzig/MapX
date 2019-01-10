using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

using Mapsui.UI.Forms;
using Mapsui.UI;

using CTForms.ViewsBase;
using CTForms.ViewModels;
using Mapsui;
using Mapsui.Projection;
using Mapsui.Utilities;
using Mapsui.Widgets;
using Mapsui.Widgets.ScaleBar;

// https://blogs.naxam.net/using-mapbox-in-xamarin-android-7ffe2466f5f7
// https://blogs.naxam.net/using-mapbox-in-xamarin-ios-ffa9bdee13f4
// https://github.com/mapbox/mapbox-gl-native/wiki/Android-4.x-to-5.0-update

namespace CTForms.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MapsuiPage : CommonToolbarPage
    {
        Random rnd = new Random();
        Func<MapView, MapClickedEventArgs, bool> clicker;

        public MapsuiPage()
        {
            On<iOS>().SetUseSafeArea(true);

            InitializeComponent();

       //     mapView.RotationLock = false;
       //     mapView.UnSnapRotationDegrees = 30;
       //     mapView.ReSnapRotationDegrees = 5;

     //       mapView.PinClicked += OnPinClicked;
     //       mapView.MapClicked += OnMapClicked;

     //       mapView.MyLocationLayer.UpdateMyLocation(new Position());

     //       location.Clicked += OnClick;

            mapControl.Map = CreateMap();
        }

        public static Map CreateMap()
        {
            var map = new Map();
            map.Layers.Add(OpenStreetMap.CreateTileLayer());
       //     map.Widgets.Add(new ScaleBarWidget(map) { TextAlignment = Alignment.Center, HorizontalAlignment = HorizontalAlignment.Center, VerticalAlignment = VerticalAlignment.Top });
       //     map.Widgets.Add(new Mapsui.Widgets.Zoom.ZoomInOutWidget { MarginX = 20, MarginY = 40 });
            return map;
        }

      //  public MapsuiPage(Action<IMapControl> setup, Func<MapView, MapClickedEventArgs, bool> c = null)
      //  {
      //      InitializeComponent();

      //      mapView.RotationLock = false;
      //      mapView.UnSnapRotationDegrees = 30;
      //      mapView.ReSnapRotationDegrees = 5;

      //      mapView.PinClicked += OnPinClicked;
      //      mapView.MapClicked += OnMapClicked;

      //      mapView.MyLocationLayer.UpdateMyLocation(new Position());

      ////      StartGPS();

      //      setup(mapView);

      //      clicker = c;
      //  }

        private void OnMapClicked(object sender, MapClickedEventArgs e)
        {
            e.Handled = clicker != null ? (bool)clicker?.Invoke(sender as MapView, e) : false;
            //Samples.SetPins(mapView, e);
            //Samples.DrawPolylines(mapView, e);
        }

        private void OnPinClicked(object sender, PinClickedEventArgs e)
        {
            if (e.Pin != null)
            {
                if (e.NumOfTaps == 2)
                {
                    // Hide Pin when double click
                    //DisplayAlert($"Pin {e.Pin.Label}", $"Is at position {e.Pin.Position}", "Ok");
                    e.Pin.IsVisible = false;
                }
                if (e.NumOfTaps == 1)
                    e.Pin.IsCalloutVisible = !e.Pin.IsCalloutVisible;
            }

            e.Handled = true;
        }

        ///// <summary>
        ///// If there was an error while getting GPS coordinates
        ///// </summary>
        ///// <param name="sender">Geolocator</param>
        ///// <param name="e">Event arguments for position error</param>
        //private void MyLocationPositionError(object sender, PositionErrorEventArgs e)
        //{
        //}

        ///// <summary>
        ///// New informations from Geolocator arrived
        ///// </summary>
        ///// <param name="sender">Geolocator</param>
        ///// <param name="e">Event arguments for new position</param>
        //private void MyLocationPositionChanged(object sender, PositionEventArgs e)
        //{
        //    Device.BeginInvokeOnMainThread(() =>
        //    {
        //        var coords = new Position(e.Position.Latitude, e.Position.Longitude);
        //        info.Text = $"{coords.ToString()} - D:{(int)e.Position.Heading} S:{Math.Round(e.Position.Speed, 2)}";

        //        mapView.MyLocationLayer.UpdateMyLocation(new Mapsui.UI.Forms.Position(e.Position.Latitude, e.Position.Longitude));
        //        mapView.MyLocationLayer.UpdateMyDirection(e.Position.Heading, mapView.Viewport.Rotation);
        //        mapView.MyLocationLayer.UpdateMySpeed(e.Position.Speed);
        //    });
        //}

        void OnPanUpdated(object sender, PanUpdatedEventArgs e)
        {
            switch (e.StatusType)
            {
                case GestureStatus.Completed:
                    ((MapViewModel)BindingContext).Panned = true;
                    break;
            }
        }

        public void OnClick(object sender, EventArgs e)
        {
 //           map.UpdateViewPort(new Position(((MapViewModel)BindingContext).Location.Lat, ((MapViewModel)BindingContext).Location.Lon), null, null, true);
        }   
    }
}

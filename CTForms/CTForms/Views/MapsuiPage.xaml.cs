using System;

using Xamarin.Forms.Xaml;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

using Mapsui;
using Mapsui.UI.Forms;
using Mapsui.Utilities;

using CTForms.Services;
using CTForms.ViewsBase;
using CTForms.ViewModels;

using KnownTileSource = CTForms.Utilities.KnownTileSource;
using KnownTileSources = CTForms.Utilities.KnownTileSources;

// https://github.com/Mapsui/Mapsui

namespace CTForms.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MapsuiPage : CommonToolbarPage
    {
        Mapsui.Layers.TileLayer activeLayer = null;

        public MapsuiPage()
        {
            On<iOS>().SetUseSafeArea(true);

            InitializeComponent();

            mapView.RotationLock = false;
            mapView.UnSnapRotationDegrees = 30;
            mapView.ReSnapRotationDegrees = 5;
            mapView.MyLocationEnabled = true;

 //           mapView.PinClicked += OnPinClicked;
 //           mapView.MapClicked += OnMapClicked;

            mapView.Map = new Map();
            ReplaceLayer(KnownTileSource.OpenCycleMap);

            Loc = LocationService.Instance;
            Loc.LocationUpdate += LocationUpdate;

            location.Clicked += OnLocatorClick;       

 //           mapView.Viewport.ViewportChanged += OnViewportChanged;
        }

        public void ReplaceLayer(KnownTileSource tileSource)
        {
            if (activeLayer != null)
            {
                mapView.Map.Layers.Remove(activeLayer);
                activeLayer = null;
            }

            var api = "";
            switch (tileSource)
            {
                case KnownTileSource.OpenCycleMap:
                case KnownTileSource.ThunderforestTransport:
                case KnownTileSource.ThunderforestOutdoors:
                case KnownTileSource.ThunderforestLandscape:
                    api = Secrets.ThunderforestSDKToken;
                    break;

                case KnownTileSource.BingAerial:
                case KnownTileSource.BingHybrid:
                case KnownTileSource.BingRoads:
                    api = Secrets.UwpMapSDKToken;
                    break;
            }
   
            activeLayer = OpenStreetMap.CreateTileLayer();
            activeLayer.TileSource = KnownTileSources.Create(tileSource, api);
            mapView.Map.Layers.Add(activeLayer);
        }

        private void LocationUpdate(object sender, EventArgs e)
        {
            var args = e as LocationEventArgs;
            var loc = args.Loc;
            mapView.MyLocationLayer.UpdateMyLocation(new Position(loc.Lat, loc.Lon));
            if (loc.Hdg != null)
                mapView.MyLocationLayer.UpdateMyDirection((double)loc.Hdg, mapView.Viewport.Rotation);
            if (loc.Spd != null)
                mapView.MyLocationLayer.UpdateMySpeed((double)loc.Spd);
        }

        //private void OnMapClicked(object sender, MapClickedEventArgs e)
        //{
        //    e.Handled = clicker != null ? (bool)clicker?.Invoke(sender as MapView, e) : false;
        //    Samples.SetPins(mapView, e);
        //    Samples.DrawPolylines(mapView, e);
        //}

        //private void OnPinClicked(object sender, PinClickedEventArgs e)
        //{
        //    if (e.Pin != null)
        //    {
        //        if (e.NumOfTaps == 2)
        //        {
        //            // Hide Pin when double click
        //            //DisplayAlert($"Pin {e.Pin.Label}", $"Is at position {e.Pin.Position}", "Ok");
        //            e.Pin.IsVisible = false;
        //        }
        //        if (e.NumOfTaps == 1)
        //            e.Pin.IsCalloutVisible = !e.Pin.IsCalloutVisible;
        //    }

        //    e.Handled = true;
        //}

        //void OnViewportChanged(object sender, EventArgs e)
        //{
        //    var viewModel = (MapViewModel)BindingContext;
        //    //     var pos = new Position(viewModel.Location.Lat, viewModel.Location.Lon).ToMapsui();
        //    var pos = mapView.MyLocationLayer.MyLocation.ToMapsui();


        //    var pos2 = mapView.Viewport.ScreenToWorld(mapView.Viewport.Center);
        //    var distance = pos.Distance(pos2);
        //    if (distance < 5)
        //    {
        //        viewModel.LocationCommand.Execute(null);
        //    }
        //    else
        //    {
        //        if (viewModel.PanCommand.CanExecute(null))
        //            viewModel.PanCommand.Execute(null);
        //    }
        //}

        public void OnLocatorClick(object sender, EventArgs e)
        {
            mapView.Navigator.CenterOn(mapView.MyLocationLayer.MyLocation.ToMapsui());
        }
    }
}

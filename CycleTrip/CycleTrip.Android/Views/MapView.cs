using Android.OS;
using Android.Runtime;
using Android.Views;
using CycleTrip.ViewModels;
using MvvmCross.Droid.Support.V4;
using MvvmCross.Droid.Views.Attributes;
using MvvmCross.Binding.Droid.BindingContext;
using Mapbox.Sdk;
using Mapbox.Sdk.Geometry;
using Mapbox.Sdk.Camera;
using Mapbox.Sdk.Maps;
using System;

// https://blogs.naxam.net/using-mapbox-in-xamarin-android-7ffe2466f5f7

namespace CycleTrip.Droid.Views
{
    [MvxFragmentPresentation(typeof(MainViewModel), Resource.Id.frameLayout, AddToBackStack = false)]
    public class MapView : MvxFragment<MapViewModel>
    {
        //        Com.Mapbox.Mapboxsdk.Maps.MapView mapView;
        Mapbox.Sdk.Maps.MapView mapView;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
           base.OnCreateView(inflater, container, savedInstanceState);

            var ret = this.BindingInflate(Resource.Layout.MapView, container, false);

            MapboxAccountManager.Start(Context, Secrets.androidMapboxToken);
            //        MapboxAccountManager.GetInstance(this, "YOUR_ACCESS_TOKEN");
            mapView = ret.FindViewById<Mapbox.Sdk.Maps.MapView>(Resource.Id.mapView);
            mapView.OnCreate(savedInstanceState);
            mapView.StyleUrl = "mapbox://styles/klzig/cjflt8g1z09kh2spsbxl3j4z2";

            var mapReadyCallback = new MyOnMapReady();
            mapReadyCallback.MapReady += (sender, args) =>
            {
                OnMapReady(mapReadyCallback.Map);
            };
            mapView.GetMapAsync(mapReadyCallback);

            return ret;
        }

        private class MyOnMapReady : Java.Lang.Object, IOnMapReadyCallback
        {
            public MapboxMap Map { get; private set; }

            public event EventHandler MapReady;

            public void OnMapReady(MapboxMap map)
            {
                Map = map;
                MapReady?.Invoke(this, EventArgs.Empty);
            }
        }

        public void OnMapReady(MapboxMap map)
        {
            var position = new CameraPosition.Builder()
                           .Target(new LatLng(43.6332, -116.216)) // Sets the new camera position
                           .Zoom(11) // Sets the zoom
                           .Build(); // Creates a CameraPosition from the builder
            map.AnimateCamera(CameraUpdateFactory.NewCameraPosition(position));
        }

        //public override void OnStart()
        //{
        //    base.OnStart();
        //    mapView.OnStart();
        //}

        public override void OnResume()
        {
            base.OnResume();
            mapView.OnResume();
        }

        public override void OnPause()
        {
            mapView.OnPause();
            base.OnPause();
        }

        public override void OnSaveInstanceState(Bundle outState)
        {
            base.OnSaveInstanceState(outState);
            mapView.OnSaveInstanceState(outState);
        }

        //public override void OnStop()
        //{
        //    base.OnStop();
        //    mapView.OnStop();
        //}

        public override void OnDestroy()
        {
            mapView.OnDestroy();
            base.OnDestroy();
        }

        public override void OnLowMemory()
        {
            base.OnLowMemory();
            mapView.OnLowMemory();
        }
    }
}

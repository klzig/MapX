using Android.OS;
using Android.Views;
using CycleTrip.ViewModels;
using MvvmCross.Droid.Support.V4;
using MvvmCross.Droid.Views.Attributes;
using MvvmCross.Binding.Droid.BindingContext;
using System;
using Com.Mapbox.Mapboxsdk;
using Com.Mapbox.Mapboxsdk.Maps;
using Com.Mapbox.Mapboxsdk.Camera;
using Com.Mapbox.Mapboxsdk.Geometry;

// https://blogs.naxam.net/using-mapbox-in-xamarin-android-7ffe2466f5f7
// https://github.com/mapbox/mapbox-gl-native/wiki/Android-4.x-to-5.0-update

namespace CycleTrip.Droid.Views
{
    [MvxFragmentPresentation(typeof(MainViewModel), Resource.Id.frameLayout, AddToBackStack = false)]
    public class MapView : MvxFragment<MapViewModel>
    {
        Com.Mapbox.Mapboxsdk.Maps.MapView mapView;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
           base.OnCreateView(inflater, container, savedInstanceState);

            var ret = this.BindingInflate(Resource.Layout.MapView, container, false);

            Mapbox.GetInstance(Context, Secrets.androidMapboxToken);
            mapView = ret.FindViewById<Com.Mapbox.Mapboxsdk.Maps.MapView>(Resource.Id.mapView);
            mapView.OnCreate(savedInstanceState);
            mapView.SetStyleUrl("mapbox://styles/klzig/cjflt8g1z09kh2spsbxl3j4z2");

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

        public override void OnStart()
        {
            base.OnStart();
            mapView.OnStart();
        }

        public override void OnStop()
        {
            base.OnStop();
            mapView.OnStop();
        }

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

        public override void OnDestroyView()
        {
            mapView.OnDestroy();
            base.OnDestroyView();
        }

        public override void OnLowMemory()
        {
            base.OnLowMemory();
            mapView.OnLowMemory();
        }
    }
}

using Android.OS;
using Android.Widget;
using Android.Views;
using MvvmCross.Droid.Support.V4;
using MvvmCross.Droid.Views.Attributes;
using MvvmCross.Binding.Droid.BindingContext;
using MvvmCross.Plugins.Messenger;
using MvvmCross.Platform;
using CycleTrip.ViewModels;
using CycleTrip.Messages;
using Com.Mapbox.Mapboxsdk;
using Com.Mapbox.Mapboxsdk.Maps;
using Com.Mapbox.Mapboxsdk.Camera;
using Com.Mapbox.Mapboxsdk.Geometry;

using static Android.Views.View;

// https://blogs.naxam.net/using-mapbox-in-xamarin-android-7ffe2466f5f7
// https://github.com/mapbox/mapbox-gl-native/wiki/Android-4.x-to-5.0-update

namespace CycleTrip.Droid.Views
{
    [MvxFragmentPresentation(typeof(MainViewModel), Resource.Id.frameLayout, AddToBackStack = false)]
    public class MapView : MvxFragment<MapViewModel>, IOnMapReadyCallback, GestureDetector.IOnGestureListener, IOnTouchListener
    {
        private static MapViewModel _viewModel;
        private static Com.Mapbox.Mapboxsdk.Maps.MapView _mapView;
        private static MapboxMap _mapboxMap;
        private static GestureDetector _gestureDetector;
        private static ImageButton _locationButton;
        private static LatLng _location = null;

        private readonly IMvxMessenger _messenger;
        private readonly MvxSubscriptionToken _locationToken;

        public MapView()
        {
            _messenger = Mvx.Resolve<IMvxMessenger>();
            _locationToken = _messenger.Subscribe<LocationMessage>(OnLocationMessage);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);
            _viewModel = ViewModel;

            var view = this.BindingInflate(Resource.Layout.MapView, container, false);

            // Set up map
            Mapbox.GetInstance(Context, Secrets.androidMapboxToken);
            _mapView = view.FindViewById<Com.Mapbox.Mapboxsdk.Maps.MapView>(Resource.Id.mapView);
            _mapView.OnCreate(savedInstanceState);
            _mapView.SetStyleUrl("mapbox://styles/klzig/cjflt8g1z09kh2spsbxl3j4z2");

            _mapView.GetMapAsync(this);
            _mapView.SetOnTouchListener(this);
            _gestureDetector = new GestureDetector(Context, this);

            // Set up bindings
            var modeButton = view.FindViewById<ImageButton>(Resource.Id.modeButton);
            modeButton.SetOnClickListener(new ModeButtonClick());
            _locationButton = view.FindViewById<ImageButton>(Resource.Id.locationButton);
            _locationButton.SetOnClickListener(new LocationButtonClick());
            var moreButton = view.FindViewById<ImageButton>(Resource.Id.moreButton);

            //var set = this.CreateBindingSet<MapView, MapViewModel>();
            //set.Bind(modeButton.ContentDescription).To(vm => vm.Mode);
            //set.Bind(locationButton.ContentDescription).To(vm => vm.Location);
            //set.Bind(moreButton.ContentDescription).To(vm => vm.More);
            //set.Apply();

            return view;
        }

        private void OnLocationMessage(LocationMessage locationMessage)
        {
            // TODO: Create map bindings and move this message handler and state to viewmodel
            _location = new LatLng(locationMessage.Lat, locationMessage.Lon);

            if (_viewModel != null && !_viewModel.Panned)
            {
                var position = new CameraPosition.Builder()
                               .Bearing(locationMessage.Hdg.GetValueOrDefault(0))
                               .Target(_location)
                               .Build();
                _mapboxMap?.AnimateCamera(CameraUpdateFactory.NewCameraPosition(position));
            }
        }

        public bool OnTouch(View v, MotionEvent e)
        {
            _gestureDetector.OnTouchEvent(e);
            return false;
        }

        public class ModeButtonClick : Java.Lang.Object, IOnClickListener
        {
            public void OnClick(View v)
            {
                var btn = v as ImageButton;
                var isMapMode = _viewModel.ModeCommand();
                if (isMapMode)
                {
                    btn.SetImageResource(Resource.Drawable.ic_list_black);
                    _mapView.Visibility = ViewStates.Visible;
                }
                else
                {
                    btn.SetImageResource(Resource.Drawable.ic_map_black);
                    _mapView.Visibility = ViewStates.Invisible;
                }
            }
        }

        public class LocationButtonClick : Java.Lang.Object, IOnClickListener
        {
            public void OnClick(View v)
            {
                _viewModel.LocationCommand();
                var btn = v as ImageButton;
                btn.SetImageResource(Resource.Drawable.ic_location_black);

                if (_location != null)
                {
                    var position = new CameraPosition.Builder()
                                   .Target(_location) // Sets the new camera position
                                   .Build(); // Creates a CameraPosition from the builder
                    _mapboxMap.AnimateCamera(CameraUpdateFactory.NewCameraPosition(position));
                }
            }
        }

        public void OnMapReady(MapboxMap map)
        {
            _mapboxMap = map;
            var position = new CameraPosition.Builder()
                           .Target(new LatLng(43.6332, -116.216)) // Sets the new camera position
                           .Zoom(11) // Sets the zoom
                           .Build(); // Creates a CameraPosition from the builder
            _mapboxMap.AnimateCamera(CameraUpdateFactory.NewCameraPosition(position));
        }

        #region View lifecycle
        public override void OnStart()
        {
            base.OnStart();
            _mapView.OnStart();
        }

        public override void OnStop()
        {
            base.OnStop();
            _mapView.OnStop();
        }

        public override void OnResume()
        {
            base.OnResume();
            _mapView.OnResume();
        }

        public override void OnPause()
        {
            _mapView.OnPause();
            base.OnPause();
        }

        public override void OnSaveInstanceState(Bundle outState)
        {
            base.OnSaveInstanceState(outState);
            _mapView.OnSaveInstanceState(outState);
        }

        public override void OnDestroyView()
        {
            _mapView.OnDestroy();
            base.OnDestroyView();
        }

        public override void OnLowMemory()
        {
            base.OnLowMemory();
            _mapView.OnLowMemory();
        }
        #endregion

        #region Gestures
        bool GestureDetector.IOnGestureListener.OnDown(MotionEvent e)
        {
            return false;
        }

        bool GestureDetector.IOnGestureListener.OnFling(MotionEvent e1, MotionEvent e2, float velocityX, float velocityY)
        {
            return true;
        }

        void GestureDetector.IOnGestureListener.OnLongPress(MotionEvent e)
        {
        }

        bool GestureDetector.IOnGestureListener.OnScroll(MotionEvent e1, MotionEvent e2, float distanceX, float distanceY)
        {
            if (_viewModel != null)
            {
                _viewModel.PanCommand();
                _locationButton.SetImageResource(Resource.Drawable.ic_location_panned_black);
            }
            return true;
        }

        void GestureDetector.IOnGestureListener.OnShowPress(MotionEvent e)
        {
        }

        bool GestureDetector.IOnGestureListener.OnSingleTapUp(MotionEvent e)
        {
            return false;
        }
        #endregion
    }
}

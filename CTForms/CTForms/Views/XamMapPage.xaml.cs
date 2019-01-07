using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

using CTForms.ViewsBase;
using CTForms.ViewModels;

// https://blogs.naxam.net/using-mapbox-in-xamarin-android-7ffe2466f5f7
// https://blogs.naxam.net/using-mapbox-in-xamarin-ios-ffa9bdee13f4
// https://github.com/mapbox/mapbox-gl-native/wiki/Android-4.x-to-5.0-update

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

            //map.DidTapOnMapCommand = new Command<Tuple<Position, Point>>((Tuple<Position, Point> obj) =>
            //{
            //    //var features = map.GetFeaturesAroundPointFunc.Invoke(obj.Item2, 6, null);
            //    //var filtered = features.Where((arg) => arg.Attributes != null);
            //    //foreach (IFeature feat in filtered)
            //    //{
            //    //    var str = JsonConvert.SerializeObject(feat);
            //    //    System.Diagnostics.Debug.WriteLine(str);
            //    //}

            //});

            //map.DidFinishLoadingStyleCommand = new Command<MapStyle>((obj) =>
            //{
            //    map.ResetPosition();
            //    foreach (Layer layer in obj.OriginalLayers)
            //    {
            //        System.Diagnostics.Debug.WriteLine(layer.Id);
            //    }

            //});
          
            //        map.ZoomLevel = Device.RuntimePlatform == Device.Android ? 4 : 10;
  //          map.ShowUserLocation = true;
  //          map.RotateEnabled = true;
  //          map.ScrollEnabled = true;

  //          var panGesture = new PanGestureRecognizer();
  //          panGesture.PanUpdated += (s, e) => {
  //              _viewModel.Panned = true;
  //          };
  //          map.GestureRecognizers.Add(panGesture);    

        }
 
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

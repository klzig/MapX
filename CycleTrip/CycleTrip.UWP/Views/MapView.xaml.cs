using MvvmCross.Uwp.Attributes;
using Windows.UI.Xaml;
using Windows.Devices.Geolocation;
using CycleTrip.Services;

namespace CycleTrip.UWP.Views
{
    [MvxRegionPresentation("ContentFrame")]
    public sealed partial class MapView : MapViewBase
    {
        public MapView()
        {
            InitializeComponent();
            Map.MapServiceToken = Secrets.UwpMapSDKToken;
            Map.ZoomLevel = 11;
            BasicGeoposition center = new BasicGeoposition
            {
                Latitude = 43.6332,
                Longitude = -116.216
            };
            Map.Center = new Geopoint(center);
            SizeChanged += MapView_Load;        
        }

        private void MapView_Load(object sender, RoutedEventArgs e)
        {
            Map.Height = ActualHeight;
            Map.Width = ActualWidth;
        }
    }
}

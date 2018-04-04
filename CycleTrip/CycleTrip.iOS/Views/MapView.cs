using MvvmCross.iOS.Views;
using MvvmCross.iOS.Views.Presenters.Attributes;
using CycleTrip.ViewModels;
using Mapbox;
using Foundation;
using CoreLocation;

// https://blogs.naxam.net/using-mapbox-in-xamarin-ios-ffa9bdee13f4

namespace CycleTrip.iOS.Views
{
    [MvxRootPresentation(WrapInNavigationController = true)]
    public partial class MapView : MvxViewController<MapViewModel>
    {
        public MapView() : base("MapView", null)
        {
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();

            // Release any cached data, images, etc that aren't in use.
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

    //        mapView.StyleURL = new NSUrl("mapbox://styles/tbd");
            mapView.SetCenterCoordinate(new CLLocationCoordinate2D(43.6332, -116.216), 11, false);
        }
    }
}
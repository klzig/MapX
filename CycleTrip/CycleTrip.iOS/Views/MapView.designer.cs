// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;

namespace CycleTrip.iOS.Views
{
    [Register ("MapView")]
    partial class MapView
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        Mapbox.MGLMapView mapView { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (mapView != null) {
                mapView.Dispose ();
                mapView = null;
            }
        }
    }
}
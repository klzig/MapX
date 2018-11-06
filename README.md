# CycleTrip
Baseline solution for a cross platform, open source mapping (OSM) application.
# Build
* To get maps to work
  * Create a MapBox account: https://www.mapbox.com/signup/
  * Create MapBox token(s), add to Secrets.cs for Android and iOS
  * UWP map page might crash without a token.
    * Create a Bing Maps account: https://www.bingmapsportal.com/
    * Create a UWP key, add it to Secrets.cs
# Design
* xamarin.android, xamarin.ios, uwp, mvvmcross, .net standard 2.0
  * Slide-over (hamburger) main menu using native, first-party controls
    * Android - Single root activity with pages as fragments; Android.Support.V7.Widget.Toolbar
    * iOS - Single root view with pages in ContentView; UINavigationBar
    * UWP - Single NavigationView with pages in ContentFrame; CommandBar
  * Shared string localization using .resx in PCL and MvvmCross ResxLocalization plugin
  * Vector images
  * MvvmCross Messenger plugin for communication between services, viewmodels, and views
  * MvvmCross Location plugin
  * Interactive vector map using MapBox iOS and Android SDKs (NAXAM bindings), Bing Maps UWP SDK
* xamarin.forms, uwp, .net standard 2.0
  * Slideover menu (master-view)
  * Shared string localization using .resx
  * Interactive vector map using MapBox iOS and Android SDKs (NAXAM forms bindings)

# MapX
Explore various Xamarin mapping solutions.
# Build
* To get maps to work without limitations
  * Create account(s) with various map providers
  * Add API token(s) for those accounts to Secrets.cs
# Design
* xamarin.forms, uwp, .net standard 2.0
  * Slideover menu (master-view)
  * Shared string localization using .resx
  * Vector images (iOS and Android)
  * Adaptive-icon for Android
  * Interactive vector map using MapBox iOS and Android SDKs (NAXAM forms bindings)
* xamarin.android, xamarin.ios, uwp, mvvmcross, .net standard 2.0
  * Slide-over (hamburger) main menu using native, first-party controls
    * Android - Single root activity with pages as fragments; Android.Support.V7.Widget.Toolbar
    * iOS - Single root view with pages in ContentView; UINavigationBar
    * UWP - Single NavigationView with pages in ContentFrame; CommandBar
  * Shared string localization using .resx in PCL and MvvmCross ResxLocalization plugin
  * Vector images
  * MvvmCross Messenger plugin for communication between services, viewmodels, and views
  * MvvmCross Location plugin
  * Interactive vector map using MapBox iOS and Android SDKs (NAXAM native bindings), Bing Maps UWP SDK

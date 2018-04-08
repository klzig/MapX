# CycleTrip
Baseline solution for a cross platform, open source mapping (OSM) application.
# Build
* To get a clean build
  * Copy CycleTrip/Secrets_template.resx to Secrets.resx
  * Add Secrets.resx to project
  * Set Access Modifier on Secrets.resx to Public
* To get maps to work
  * Create a MapBox account: https://www.mapbox.com/signup/
  * Create a MapBox token, add it to Secrets.resx
  * UWP maps will display without a token.  To remove license banner:
  * Create a Bing Maps account: https://www.bingmapsportal.com/
  * Create a UWP key, add it to Secrets.resx
# Design
* xamarin.android, xamarin.ios, uwp, mvvmcross
* Slide-over (hamburger) main menu using native, first-party controls
  * Android - Single root activity with pages as fragments; Android.Support.V7.Widget.Toolbar
  * iOS - Single root view with pages in ContentView; UINavigationBar
  * UWP - Single NavigationView with pages in ContentFrame; CommandBar
* Shared string localization using .resx in PCL and MvvmCross ResxLocalization plugin
* Vector images
* MvvmCross Messenger plugin for communication between services, viewmodels, and views
* MvvmCross Location plugin
* Interactive vector map using MapBox iOS and Android SDKs, Bing Maps UWP SDK

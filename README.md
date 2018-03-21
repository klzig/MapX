# CycleTrip
Baseline solution for a cross platform, open source mapping (OSM) application

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

﻿using Android.OS;
using Android.Runtime;
using Android.Views;
using CycleTrip.ViewModels;
using MvvmCross.Droid.Support.V4;
using MvvmCross.Droid.Views.Attributes;

namespace CycleTrip.Droid.Views
{
    [MvxFragmentPresentation(typeof(MainViewModel), Resource.Id.frameLayout, AddToBackStack = true)]
    public class SecondPageView : MvxFragment<SecondPageViewModel>
    {
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            return inflater.Inflate(Resource.Layout.SecondPageView, container, false);
        }
    }
}
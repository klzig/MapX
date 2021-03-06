﻿using MvvmCross.iOS.Views;
using MvvmCross.iOS.Views.Presenters.Attributes;
using CycleTrip.ViewModels;

namespace CycleTrip.iOS.Views
{
    [MvxRootPresentation(WrapInNavigationController = true)]
    public partial class InfoView : MvxViewController<InfoViewModel>
    {
        public InfoView() : base("InfoView", null)
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

            // Perform any additional setup after loading the view, typically from a nib.
        }
    }
}
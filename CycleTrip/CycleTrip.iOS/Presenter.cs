using MvvmCross.Core.ViewModels;
using MvvmCross.iOS.Platform;
using MvvmCross.iOS.Views;
using MvvmCross.iOS.Views.Presenters;
using MvvmCross.iOS.Views.Presenters.Attributes;
using System;
using System.Collections.Generic;
using System.Text;
using UIKit;

namespace CycleTrip.iOS
{
    public class ContainerPresenter : MvxIosViewPresenter
    {
        public ContainerPresenter(IMvxApplicationDelegate applicationDelegate, UIWindow window)
            : base(applicationDelegate, window)
        {
        }

        protected override MvxNavigationController CreateNavigationController(UIViewController viewController)
        {
            var toReturn = base.CreateNavigationController(viewController);
            toReturn.NavigationBarHidden = false;
            toReturn.NavigationBar.TintColor = UIColor.FromRGB(15, 79, 140);
            toReturn.NavigationBar.BarTintColor = UIColor.FromRGB(228, 242, 231);
            toReturn.NavigationBar.Translucent = false;
            return toReturn;
        }

        protected override void SetWindowRootViewController(UIViewController controller, MvxRootPresentationAttribute attribute = null)
        {
  //          this.CreateViewControllerFor
            base.SetWindowRootViewController(controller, attribute);
        }

        //protected override void ShowChildViewController(UIViewController viewController, MvxChildPresentationAttribute attribute, MvxViewModelRequest request)
        //{
        //    base.ShowChildViewController(viewController, attribute, request);
        //}
    }
}

using MvvmCross.Core.ViewModels;
using MvvmCross.iOS.Platform;
using MvvmCross.iOS.Views;
using MvvmCross.iOS.Views.Presenters;
using MvvmCross.iOS.Views.Presenters.Attributes;
using System;
using System.Collections.Generic;
using System.Text;
using UIKit;
using CycleTrip.iOS.Views;

// http://www.allenhashkey.com/mobile-development/adding-a-view-controller-to-a-container-view-in-xamarin-ios/

namespace CycleTrip.iOS
{
    public class ContainerPresenter : MvxIosViewPresenter
    {
        private UIView _containerView;
        private MvxNavigationController _navigationController;
        //     private MvxViewController _rootController;  // How does this work?
        //     MvxViewController c = _rootController.CreateViewControllerFor(request) as MvxViewController;

        public ContainerPresenter(IMvxApplicationDelegate applicationDelegate, UIWindow window)
            : base(applicationDelegate, window)
        {
        }

       // public override void Show(MvxViewModelRequest request)
       // {
       //     base.Show(request);

       ////     if (_navigationController != null)
       ////     {
       ////         //the view controller we want to insert into the container
       ////         //var viewController = new MyViewController();  // viewController == c

       ////         //c.View.Frame = this._containerView.Bounds;
       ////         //c.WillMoveToParentViewController(_navigationController.TopViewController);
       ////         //_containerView.AddSubview(c.View);
       ////         //_navigationController.TopViewController.AddChildViewController(c);
       ////         //c.DidMoveToParentViewController(_navigationController.TopViewController);




       ////         //         _navigationController.NavigationItem.SetHidesBackButton(true, true);

       //////         PushViewControllerIntoStack(_navigationController, c, true);

       ////         ShowRootViewController(c, new MvxRootPresentationAttribute() { WrapInNavigationController = false }, request);

       ////     }
       ////     else
       ////     {
       ////         base.Show(request);
       ////     }
       //  //   base.Show(request);
       // }

        protected override MvxNavigationController CreateNavigationController(UIViewController viewController)
        {
            // Wrap MainView in navigation controller
            var toReturn = base.CreateNavigationController(viewController);
            toReturn.NavigationBarHidden = false;
            toReturn.NavigationBar.TintColor = UIColor.FromRGB(15, 79, 140);
            toReturn.NavigationBar.BarTintColor = UIColor.FromRGB(228, 242, 231);
            toReturn.NavigationBar.Translucent = false;
            //           toReturn.NavigationItem.SetLeftBarButtonItem(new UIBarButtonItem(UIImage.FromFile("menu_icon.png"), UIBarButtonItemStyle.Plain, null), true);
            _navigationController = toReturn;
            return toReturn;
        }

        protected override void SetWindowRootViewController(UIViewController controller, MvxRootPresentationAttribute attribute = null)
        {
            if (_window.RootViewController == null)
            {
                // Insert MainView as root controller, subsequent root menu_item controllers will use MainView.ContainerView as root
                base.SetWindowRootViewController(controller, attribute);

                // Hack to get around: 'MainView.ContainerView' is inacessible due to its protection level
                // _containerView = (_window.RootViewController as MainView).ContainerView;
                _containerView = _window.RootViewController.View.ViewWithTag(1);
            }
            else
            {
                // Move root menu_item controller to ContainerView
                controller.View.Frame = _containerView.Bounds;
                controller.WillMoveToParentViewController(_window.RootViewController);
                _containerView.AddSubview(controller.View);
                _window.RootViewController.AddChildViewController(controller);
                controller.DidMoveToParentViewController(_window.RootViewController);
            }
        }
    }
}

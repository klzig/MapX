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
        private MvxViewController _rootController;

        public ContainerPresenter(IMvxApplicationDelegate applicationDelegate, UIWindow window)
            : base(applicationDelegate, window)
        {
        }

        public override void Show(MvxViewModelRequest request)
        {
            if (_navigationController == null)
            {
                // First controller called from main hamburger menu.  Must have decorator: WrapInNavigationController = true
                base.Show(request);
            }
            else
            {
                // Subsequent controllers called from main menu or other views 
                MvxViewController c = _rootController.CreateViewControllerFor(request) as MvxViewController;
                string val = "";
                if (request.PresentationValues != null)
                {
                    request.PresentationValues.TryGetValue("NavigationMode", out val);
                }
                if (val.Equals("Push", StringComparison.Ordinal))
                {
                    // Push new controller onto navigation stack
                    PushViewControllerIntoStack(_navigationController, c, true);
                }
                else
                {
                    // Replace stack in existing navigation controller with new controller
                    UIViewController[] controllers = {c};
                    _navigationController.SetViewControllers(controllers, true);
                }
            }
        }

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
                _rootController = controller as MvxViewController;

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

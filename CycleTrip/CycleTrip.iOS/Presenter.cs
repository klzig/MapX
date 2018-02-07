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
using CoreGraphics;

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

                    // Apparently SetViewControllers unloads all the toolbar buttons, etc.  Seems very wrong...
                    _navigationController.SetViewControllers(controllers, false);
                    PopulateNavigationBar(_navigationController);
                }
            }
        }

        protected override MvxNavigationController CreateNavigationController(UIViewController viewController)
        {
            // One NavigationController instance for all views
            _navigationController = base.CreateNavigationController(viewController);
            _navigationController.NavigationBarHidden = false;
            _navigationController.NavigationBar.TintColor = UIColor.FromRGB(15, 79, 140);
            _navigationController.NavigationBar.BarTintColor = UIColor.FromRGB(228, 242, 231);
            _navigationController.NavigationBar.Translucent = false;

            PopulateNavigationBar(_navigationController);
     
            return _navigationController;
        }

        private void PopulateNavigationBar(UINavigationController nav)
        {
            UIBarButtonItem btn = new UIBarButtonItem();
            btn.Image = UIImage.FromFile("hamburger.png");
            btn.Clicked += (sender, e) => { System.Diagnostics.Debug.WriteLine("Button tap"); };
            //       UINavigationItem _navigationItem = new UINavigationItem();
            //       _navigationItem.RightBarButtonItem = btn;
            nav.NavigationBar.TopItem.SetLeftBarButtonItem(btn, true);
            nav.NavigationBar.TopItem.Title = "My Title";

            // Left-justify title
            //         CGRect frame = toReturn.NavigationItem.TitleView.Frame;  // Why is TitleView null?
            //         frame.X = 10;
            //         toReturn.NavigationItem.TitleView.Frame = frame;
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

using MvvmCross.Binding.BindingContext;
using MvvmCross.iOS.Views;
using MvvmCross.iOS.Views.Presenters.Attributes;
using CycleTrip.ViewModels;
using System;
using System.Collections.Generic;
using UIKit;
using MvvmCross.Plugins.Messenger;
using MvvmCross.Platform;
using CycleTrip.Messages;
using CoreAnimation;
using CycleTrip.iOS.UserControls;
using CoreGraphics;

// Flyout menu adapted from http://www.appliedcodelog.com/2015/09/sliding-menu-in-xamarinios-using.html
// Github https://github.com/suchithm/SlidingMenu_Xamarin.iOS

namespace CycleTrip.iOS.Views
{
    [MvxRootPresentation(WrapInNavigationController = false)]
    public partial class MainView : MvxViewController<MainViewModel>
    {
        private MenuTableSourceClass _menuTableSource;
 //       private nfloat _viewShiftUpY;
 //       private nfloat _viewBringDownY;

        // Hold on to tokens
        private readonly MvxSubscriptionToken _alertToken;  
        private readonly MvxSubscriptionToken _titleToken;

        public MainView() : base("MainView", null)
        {
            IMvxMessenger messenger = Mvx.Resolve<IMvxMessenger>();
            _alertToken = messenger.Subscribe<AlertMessage>(OnAlertMessage);
            _titleToken = messenger.Subscribe<ViewTitleMessage>(OnViewTitleMessage);

            AlertItem.Init();
            AlertItem.Alerts[AlertType.notification].Button.Clicked += (sender, e) => { ViewModel.NavigateTo(2); };

            HamburgerItem.Init();
            HamburgerItem.Button.Clicked += (sender, e) => { PerformTableTransition(); };
        }

        public MainView(IntPtr handle) : base(handle)
        {
        }

        #region View lifecycle

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            //var set = this.CreateBindingSet<MainView, MainViewModel>();
            //set.Bind(TextField).To(vm => vm.Text);
            //set.Apply();

            MenuItem.Init(ViewModel.ModelMenuItems);

            InitializeView();
            tableViewLeftMenu.Hidden = true;
            BindMenu();
        }

        public override void ViewWillTransitionToSize(CGSize toSize, IUIViewControllerTransitionCoordinator coordinator)
        {
            // TODO: Adjust top postion of menu to height of statusbar + navigationbar
            base.ViewWillTransitionToSize(toSize, coordinator);
        }

        //public override void DidReceiveMemoryWarning()
        //{
        //    base.DidReceiveMemoryWarning();
        //    // Release any cached data, images, etc that aren't in use.
        //}

        #endregion

        #region NavigationBar

        private static void OnAlertMessage(AlertMessage alert)
        {
            AlertItem.SetVisibility(alert.Type, alert.Visible);
            ContainerPresenter.NavigationController.NavigationBar.TopItem.SetRightBarButtonItems(AlertItem.GetAlertItems(), false);
        }

        private static void OnViewTitleMessage(ViewTitleMessage msg)
        {
            ContainerPresenter.NavigationController.NavigationBar.TopItem.Title = msg.Title;
        }

        public class HamburgerItem
        {
            public static UIBarButtonItem Button { get; set; }

            public static void Init()
            {
                Button = new UIBarButtonItem
                {
                    Image = UIImage.FromBundle("Hamburger"),
                };
            }
        }

        public class AlertItem
        {
            public static Dictionary<AlertType, AlertItem> Alerts;
      
            public UIBarButtonItem Button { get; set; }
            public bool Visible { get; set; }

            public static void Init()
            {
                Alerts = new Dictionary<AlertType, AlertItem> {
                    { AlertType.notification, new AlertItem
                        {
                            Button = new UIBarButtonItem {  Image = UIImage.FromBundle("Notifications") },
                            Visible = false
                        }
                    }
                };
            }

            public static void SetVisibility(AlertType type, bool visible)
            {
                Alerts[type].Visible = visible;
            }

            public static UIBarButtonItem[] GetAlertItems()
            {
                var items = new List<UIBarButtonItem>();

                foreach (KeyValuePair<AlertType, AlertItem> x in Alerts)
                {
                    if (x.Value.Visible)
                    {
                        items.Add(x.Value.Button);
                    }
                }
                return items.ToArray();
            }
        }

        #endregion

        #region Flyout menu

        public class MenuItem
        {
            public static List<MenuItem> Items { get; set; }
            public string IconPath { get; set; }
            public string MenuName { get; set; }

            public static void Init(ModelMenuItem[] menuItems)
            {
                Items = new List<MenuItem>
                {
                    new MenuItem() { IconPath = "FirstPage", MenuName = menuItems[0].MenuName },
                    new MenuItem() { IconPath = "MapPage", MenuName = menuItems[1].MenuName },
                    new MenuItem() { IconPath = "Notifications", MenuName = menuItems[2].MenuName },
                    new MenuItem() { IconPath = "Settings", MenuName = menuItems[3].MenuName }
                };
            }
        }

        public class MenuTableSourceClass : UITableViewSource
        {
            internal event Action<int> MenuSelected;

            public override nint RowsInSection(UITableView tableview, nint section)
            {
                return MenuItem.Items.Count;
            }

            public override UITableViewCell GetCell(UITableView tableView, Foundation.NSIndexPath indexPath)
            {
                var cell = tableView.DequeueReusableCell(LeftDrawerMenuItemView.Key) as LeftDrawerMenuItemView ?? LeftDrawerMenuItemView.Create();
                cell.BindData(MenuItem.Items[indexPath.Row].MenuName, MenuItem.Items[indexPath.Row].IconPath);
                return cell;
            }

            public override void RowSelected(UITableView tableView, Foundation.NSIndexPath indexPath)
            {
                MenuSelected?.Invoke(indexPath.Row);
                tableView.DeselectRow(indexPath, true);
            }

            public override UIView GetViewForFooter(UITableView tableView, nint section)
            {
                return new UIView();
            }
        }

        private void InitializeView()
        {
            var recognizerRight = new UISwipeGestureRecognizer(SwipeLeftToRight)
            {
                Direction = UISwipeGestureRecognizerDirection.Right
            };
            View.AddGestureRecognizer(recognizerRight);

            var recognizerLeft = new UISwipeGestureRecognizer(SwipeRightToLeft)
            {
                Direction = UISwipeGestureRecognizerDirection.Left
            };
            View.AddGestureRecognizer(recognizerLeft);
        }

        private void SwipeLeftToRight()
        {
            if (tableViewLeftMenu.Hidden)
                PerformTableTransition();
        }

        private void SwipeRightToLeft()
        {
            if (!tableViewLeftMenu.Hidden)
                PerformTableTransition();
        }

        private void PerformTableTransition()
        {
            tableViewLeftMenu.Hidden = !tableViewLeftMenu.Hidden;
            var transition = new CATransition
            {
                Duration = 0.25f,
                Type = CAAnimation.TransitionPush
            };
            if (tableViewLeftMenu.Hidden)
            {
                transition.TimingFunction = CAMediaTimingFunction.FromName(new Foundation.NSString("easeOut"));
                transition.Subtype = CAAnimation.TransitionFromRight;
            }
            else
            {
                transition.TimingFunction = CAMediaTimingFunction.FromName(new Foundation.NSString("easeIn"));
                transition.Subtype = CAAnimation.TransitionFromLeft;
            }
            tableViewLeftMenu.Layer.AddAnimation(transition, null);
        }

        private void BindMenu()
        {
            if (_menuTableSource != null)
            {
                _menuTableSource.MenuSelected -= MenuSelected;
                _menuTableSource = null;
            }
            _menuTableSource = new MenuTableSourceClass();
            _menuTableSource.MenuSelected += MenuSelected;
            tableViewLeftMenu.Source = _menuTableSource;
        }

        private void MenuSelected(int menuSelected)
        {
            //          txtActionBarText.Text = menuSeleted;
            ViewModel.NavigateTo(menuSelected);
            SwipeRightToLeft();
        }

        //private void AnimateView(nfloat frameY, UIView view)
        //{
        //    UIView.Animate(0.2f, 0.1f, UIViewAnimationOptions.CurveEaseIn, delegate
        //    {
        //        var frame = View.Frame;
        //        frame.Y = frameY;
        //        view.Frame = frame;
        //    }, null);
        //}

        #endregion
    }
}

using MvvmCross.Binding.BindingContext;
using MvvmCross.iOS.Views;
using MvvmCross.iOS.Views.Presenters.Attributes;
using CycleTrip.ViewModels;
using System;
//using System.Collections.Generic;
//using UIKit;
//using MvvmCross.Plugins.Messenger;
//using MvvmCross.Platform;
//using CycleTrip.Messages;
//using CycleTrip.iOS;
//using System.Collections;

namespace CycleTrip.iOS.Views
{
    [MvxRootPresentation(WrapInNavigationController = false)]
    public partial class MainView : MvxViewController<MainViewModel>
    {
        //Dictionary<AlertType, UIBarButtonItem> _alerts = new Dictionary<AlertType, UIBarButtonItem> {
        //    {AlertType.notification, null},
        //    {AlertType.recording, null }
        //};

        //private static Dictionary<AlertType, AlertItem> Alerts = new Dictionary<AlertType, AlertItem> {
        //    { AlertType.recording,  new AlertItem
        //        {
        //            Button = new UIBarButtonItem { Image = UIImage.FromFile("alert.png") },
        //            Visible = false
        //        }
        //    },
        //    { AlertType.notification, new AlertItem
        //        {
        //            Button = new UIBarButtonItem { Image = UIImage.FromFile("alert.png") },
        //            Visible = false
        //        }
        //    }
        //};

//        private readonly IMvxMessenger _messenger;
//        private readonly MvxSubscriptionToken _alert_token;
//        private readonly MvxSubscriptionToken _title_token;

        public MainView() : base("MainView", null)
        {
    //        _messenger = Mvx.Resolve<IMvxMessenger>();
    //        _alert_token = _messenger.Subscribe<AlertMessage>(OnAlertMessage);
    //        _title_token = _messenger.Subscribe<ViewTitleMessage>(OnViewTitleMessage);

        }

        public MainView(IntPtr handle) : base(handle)
        {
        }

 //       private void OnAlertMessage(AlertMessage alert)
  //      {
 //           AlertItem.SetVisibility(alert.Type, alert.Visible);
  //          ContainerPresenter.NavigationController.NavigationBar.TopItem.SetRightBarButtonItems(AlertItem.GetAlertItems(), false);
  //      }

  //      private void OnViewTitleMessage(ViewTitleMessage msg)
  //      {
   //         var loader = new Windows.ApplicationModel.Resources.ResourceLoader();
   //         string title = loader.GetString(msg.Title);
   //         hamburgerMenuControl.Header = title;
   //         ContainerPresenter.NavigationController.NavigationBar.TopItem.Title = msg.Title;
    //    }

        //public class AlertItem
        //{
        //    public UIBarButtonItem Button { get; set; }
        //    public bool Visible { get; set; }

        //    public static void SetVisibility(AlertType type, bool visible)
        //    {
        //        Alerts[type].Visible = visible;
        //    }

        //    public static UIBarButtonItem[] GetAlertItems()
        //    {
        //        var items = new List<UIBarButtonItem>();

        //        foreach (KeyValuePair<AlertType, AlertItem> x in Alerts)
        //        {
        //            if (x.Value.Visible)
        //            {
        //                items.Add(x.Value.Button);
        //            }
        //        }
        //        return items.ToArray();
        //    }
        //}

        #region View lifecycle

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            var set = this.CreateBindingSet<MainView, MainViewModel>();
            set.Bind(TextField).To(vm => vm.Text);
            set.Bind(Button).To(vm => vm.ResetTextCommand);
            set.Bind(FirstPageButton).To(vm => vm.FirstPageCommand);
            set.Bind(SecondPageButton).To(vm => vm.SecondPageCommand);
            set.Bind(InfoButton).To(vm => vm.InfoCommand);
            set.Bind(SettingsButton).To(vm => vm.SettingsCommand);
            set.Apply();
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
        }

        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);
        }

        public override void ViewWillDisappear(bool animated)
        {
            base.ViewWillDisappear(animated);
        }

        public override void ViewDidDisappear(bool animated)
        {
            base.ViewDidDisappear(animated);
        }

        #endregion
    }
}

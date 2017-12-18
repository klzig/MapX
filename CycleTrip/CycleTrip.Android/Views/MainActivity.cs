﻿using Android.App;
using Android.OS;
using Android.Support.V4.Widget;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using CycleTrip.Messages;
using CycleTrip.ViewModels;
using MvvmCross.Droid.Support.V7.AppCompat;
using MvvmCross.Platform;
using MvvmCross.Plugins.Messenger;
using System.Collections.Generic;
using System.Linq;
using Toolbar = Android.Support.V7.Widget.Toolbar;

// https://developer.xamarin.com/guides/android/user_interface/layouts/list-view/part-3-customizing-list-view-appearance/

namespace CycleTrip.Droid.Views
{
    /// <summary>
    /// The one activity.  It managages the top navigation bar, navigation drawer, and hosts pages as fragments. 
    /// </summary>
    [Activity]
    public class MainActivity : MvxAppCompatActivity<MainViewModel>
    {
        ActionBarDrawerToggle _drawerToggle;
        ListView _drawerListView;
        DrawerLayout _drawerLayout;
        string _title;

        Dictionary<AlertType, bool> _alerts = new Dictionary<AlertType, bool> {
            {AlertType.notification, false},
            {AlertType.recording, false }
        };

        public IEnumerable<int> MenuIcons{ get; private set; } = new int[] { Resource.Drawable.ic_android_black, Resource.Drawable.ic_settings_black };
        private readonly IMvxMessenger _messenger;
        private readonly MvxSubscriptionToken _alert_token;
        private readonly MvxSubscriptionToken _title_token;

        public MainActivity() : base()
        {
            _messenger = Mvx.Resolve<IMvxMessenger>();
            _alert_token = _messenger.Subscribe<AlertMessage>(OnAlertMessage);
            _title_token = _messenger.Subscribe<ViewTitleMessage>(OnViewTitleMessage);
        }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.Main);

            var toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            SupportActionBar.SetDisplayShowHomeEnabled(true);

            _drawerListView = FindViewById<ListView>(Resource.Id.drawerListView);
            _drawerListView.ItemClick += (s, e) => ShowFragmentAt(e.Position);
            _drawerListView.Adapter = new MenuListAdapter(this, ViewModel.MenuItems.ToList());
            _drawerLayout = FindViewById<DrawerLayout>(Resource.Id.drawerLayout);
            _drawerToggle = new ActionBarDrawerToggle(this, _drawerLayout, Resource.String.OpenDrawerString, Resource.String.CloseDrawerString);

            // Platform-specific initialization
            ViewModel.MenuItems[0].IconId = Resource.Drawable.ic_android_black;
            ViewModel.MenuItems[1].IconId = Resource.Drawable.ic_settings_black;

            ShowFragmentAt(0);

            ViewModel.SelfTest();
        }

        void ShowFragmentAt(int position)
        {
            ViewModel.NavigateTo(position);
            _drawerLayout.CloseDrawer(_drawerListView);
        }

        protected override void OnPostCreate(Bundle savedInstanceState)
        {
            _drawerToggle.SyncState();

            base.OnPostCreate(savedInstanceState);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            if (_drawerToggle.OnOptionsItemSelected(item))
                return true;

            return base.OnOptionsItemSelected(item);
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {   // Shared toolbar alert buttons
            IMenuItem alert;
            MenuInflater.Inflate(Resource.Layout.Toolbar, menu);

            alert = menu.FindItem(Resource.Id.notification);
            alert.SetVisible(_alerts[AlertType.notification]);

            alert = menu.FindItem(Resource.Id.recording);
            alert.SetVisible(_alerts[AlertType.recording]);

            return base.OnCreateOptionsMenu(menu);
        }

        private void OnAlertMessage(AlertMessage alert)
        {
            _alerts[alert.Type] = alert.Visible;
            InvalidateOptionsMenu();
        }

        private void OnViewTitleMessage(ViewTitleMessage msg)
        {
            _title = Title = msg.Title;
            InvalidateOptionsMenu();
        }
    }

    public class MenuListAdapter : BaseAdapter<MenuItem>
    {
        List<MenuItem> items;
        Activity context;

        // Crashing here running debug: Java.Lang.ClassNotFoundException
        // Fixed by bumping minimum API target to >= Lolipop per:
        // https://github.com/jamesmontemagno/CurrentActivityPlugin/issues/1
        public MenuListAdapter(Activity context, List<MenuItem> items)
            : base()
        {
            this.context = context;
            this.items = items;
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override MenuItem this[int position]
        {
            get { return items[position]; }
        }

        public override int Count
        {
            get { return items.Count; }
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var item = items[position];
            View view = convertView;
            if (view == null)
                // No view to re-use, create new
                view = context.LayoutInflater.Inflate(Resource.Layout.MenuItem, null);
            view.FindViewById<TextView>(Resource.Id.menu_name).Text = item.MenuName;
            view.FindViewById<ImageView>(Resource.Id.menu_icon).SetImageResource(item.IconId);
            return view;
        }
    }
}
using Android.App;
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
using Toolbar = Android.Support.V7.Widget.Toolbar;

// https://developer.xamarin.com/guides/android/user_interface/layouts/list-view/part-3-customizing-list-view-appearance/

namespace CycleTrip.Droid.Views
{
    /// <summary>
    /// The one activity, common to all views.  It managages the top navigation bar, navigation drawer, and hosts views as fragments. 
    /// </summary>
    [Activity]
    public class MainView : MvxAppCompatActivity<MainViewModel>
    {
        MyActionBarDrawerToggle _drawerToggle;
        ListView _drawerListView;
        DrawerLayout _drawerLayout;

        Dictionary<AlertType, bool> _alerts = new Dictionary<AlertType, bool> {
            {AlertType.notification, false},
            {AlertType.recording, false }
        };

        private readonly IMvxMessenger _messenger;
        private readonly MvxSubscriptionToken _alert_token;
        private readonly MvxSubscriptionToken _title_token;

        public MainView() : base()
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
            _drawerListView.Adapter = new MenuListAdapter(this, MenuItem.GetMenuItems(ViewModel.ModelMenuItems));
            _drawerLayout = FindViewById<DrawerLayout>(Resource.Id.drawerLayout);
            _drawerToggle = new MyActionBarDrawerToggle(this, _drawerLayout, toolbar, Resource.String.OpenDrawerString, Resource.String.CloseDrawerString);
            _drawerLayout.AddDrawerListener(_drawerToggle);
            _drawerToggle.SyncState();
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

            switch (item.ItemId)
            {
                case Resource.Id.notification:
                    // TODO: Figure out how to add this view to backstack when navigating via actionbar button
                    ViewModel.NavigateTo(2);
                    break;
            }

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
            SupportActionBar.SetWindowTitle(msg.Title);
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

    // This approach is wrong?  https://forums.xamarin.com/discussion/54407/explicit-interface-implementation-for-drawerlayout
    // This class has one purpose: change the appbar title to the app name when the navigation drawer is open
    public class MyActionBarDrawerToggle : ActionBarDrawerToggle
    {
        private AppCompatActivity _HostActivity;

        public MyActionBarDrawerToggle(AppCompatActivity host, DrawerLayout drawerLayout, Toolbar toolbar, int openedResource, int closedResource)
            : base(host, drawerLayout, toolbar, openedResource, closedResource)
        {
            _HostActivity = host;
        }

        //public override void OnDrawerOpened(View drawerView)
        //{
        //    int drawerType = (int)drawerView.Tag;

        //    base.OnDrawerOpened(drawerView);

        //    if (drawerType == 0)
        //    {
        //        base.OnDrawerOpened(drawerView);
        //         // base.OnDrawerSlide(drawerView, 0);  // Disables the back arrow
        //    }
        //}

        public override void OnDrawerClosed(View drawerView)
        {
            int drawerType = (int)drawerView.Tag;

            base.OnDrawerClosed(drawerView);

            if (drawerType == 0)
            {
                base.OnDrawerClosed(drawerView);
            }
        }

        //public override void OnDrawerSlide(View drawerView, float slideOffset)
        //{
        //    base.OnDrawerSlide(drawerView, 0); // Disables the animation 
        //}
    }

    /// <summary>
    /// An Android main menu listview item
    /// </summary>
    public class MenuItem
    {
        public int IconId { get; set; }
        public string MenuName { get; set; }

        public static List<MenuItem> GetMenuItems(ModelMenuItem[] menuItems)
        {
            var items = new List<MenuItem>();

            items.Add(new MenuItem() { IconId = Resource.Drawable.ic_android_black, MenuName = menuItems[0].MenuName });
            items.Add(new MenuItem() { IconId = Resource.Drawable.ic_map_black, MenuName = menuItems[1].MenuName });
            items.Add(new MenuItem() { IconId = Resource.Drawable.ic_info_outline_black, MenuName = menuItems[2].MenuName });
            items.Add(new MenuItem() { IconId = Resource.Drawable.ic_location_black, MenuName = menuItems[3].MenuName });
            items.Add(new MenuItem() { IconId = Resource.Drawable.ic_settings_black, MenuName = menuItems[4].MenuName });

            return items;
        }
    }
}
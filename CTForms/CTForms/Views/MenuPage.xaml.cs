using System;
using System.Collections.Generic;
using System.Reflection;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using CTForms.Models;

namespace CTForms.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MenuPage : ContentPage
    {
        MainPage RootPage { get => Application.Current.MainPage as MainPage; }
        List<HomeMenuItem> menuItems;
        public MenuPage()
        {
            InitializeComponent();

            menuItems = new List<HomeMenuItem>
            {
                new HomeMenuItem {Id = MenuItemType.Browse, Title=Properties.Resources.MenuItemBrowse, Icon=""},
                new HomeMenuItem {Id = MenuItemType.Map, Title=Properties.Resources.MenuItemMap, Icon="ic_map_black"},
                new HomeMenuItem {Id = MenuItemType.About, Title=Properties.Resources.MenuItemAbout, Icon="ic_settings_black" }
            };

            // NOTE: use for debugging, not in released app code!
            //var assembly = typeof(MenuPage).GetTypeInfo().Assembly;
            //foreach (var res in assembly.GetManifestResourceNames())
            //    System.Diagnostics.Debug.WriteLine("found resource: " + res);

            ListViewMenu.ItemsSource = menuItems;

            ListViewMenu.SelectedItem = menuItems[0];
            ListViewMenu.ItemSelected += async (sender, e) =>
            {
                if (e.SelectedItem == null)
                    return;

                var id = (int)((HomeMenuItem)e.SelectedItem).Id;
                await RootPage.NavigateFromMenu(id);
            };
        }
    }
}
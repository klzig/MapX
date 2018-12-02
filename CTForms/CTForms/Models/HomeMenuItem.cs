using System;
using System.Collections.Generic;
using System.Text;

namespace CTForms.Models
{
    public enum MenuItemType
    {
        Browse,
        Map,
        About,
        Notifications,
        Location
    }
    public class HomeMenuItem
    {
        public MenuItemType Id { get; set; }

        public string Title { get; set; }

        public string Icon { get; set; }
    }
}

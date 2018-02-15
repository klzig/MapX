using System;
using Foundation;
using UIKit;

namespace CycleTrip.iOS.UserControls
{
    public partial class LeftDrawerMenuItemView : UITableViewCell
    {
        public static readonly NSString Key = new NSString("LeftDrawerMenuItemView");
        public static readonly UINib Nib;

        protected LeftDrawerMenuItemView(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }

        static LeftDrawerMenuItemView()
        {
            Nib = UINib.FromName("LeftDrawerMenuItemView", NSBundle.MainBundle);
        }

        public static LeftDrawerMenuItemView Create()
        {
            return (LeftDrawerMenuItemView)Nib.Instantiate(null, null)[0];
        }

        internal void BindData(string label, string iconPath)
        {
            menuText.Text = label;
            viewMenuIcon.Image = UIImage.FromBundle(iconPath);
        }
    }
}
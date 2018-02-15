// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;

namespace CycleTrip.iOS.UserControls
{
    [Register ("LeftDrawerMenuItemView")]
    partial class LeftDrawerMenuItemView
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel menuText { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIImageView viewMenuIcon { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (menuText != null) {
                menuText.Dispose ();
                menuText = null;
            }

            if (viewMenuIcon != null) {
                viewMenuIcon.Dispose ();
                viewMenuIcon = null;
            }
        }
    }
}
using Android.Support.V7.Graphics.Drawable;
using ImageButton = Android.Widget.ImageButton;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android.AppCompat;
using CTForms.Droid.Renderers;

// Partially obscuring most of the hamburger animation with the menu pane is tacky.  Kill the animation.

[assembly: ExportRenderer(typeof(MasterDetailPage), typeof(CustomIconRenderer))]
namespace CTForms.Droid.Renderers
{
#pragma warning disable CS0618 // Type or member is obsolete
    public class CustomIconRenderer : MasterDetailPageRenderer
    {
        protected override void OnLayout(bool changed, int l, int t, int r, int b)
        {
            base.OnLayout(changed, l, t, r, b);
            var toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            if (toolbar != null)
            {
                for (var i = 0; i < toolbar.ChildCount; i++)
                {
                    var imageButton = toolbar.GetChildAt(i) as ImageButton;
                    if (!(imageButton?.Drawable is DrawerArrowDrawable drawerArrow))
                        continue;

                    bool displayBack = false;
                    var app = Application.Current;

                    var detailPage = (app.MainPage as MasterDetailPage).Detail;
                    var navPageLevel = detailPage.Navigation.NavigationStack.Count;
                    if (navPageLevel > 1)
                        displayBack = true;

                    if (!displayBack)
                        ChangeIcon(imageButton, Resource.Drawable.hamburger);
                }
            }
        }

        private void ChangeIcon(ImageButton imageButton, int id)
        {
            if (Android.OS.Build.VERSION.SdkInt >= Android.OS.BuildVersionCodes.Lollipop)
                imageButton.SetImageDrawable(Context.GetDrawable(id));
            imageButton.SetImageResource(id);
        }
    }
}
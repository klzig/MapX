using MvvmCross.Uwp.Attributes;

namespace CycleTrip.UWP.Views
{
    [MvxRegionPresentation("ContentFrame")]
    public sealed partial class FirstPageView : FirstPageViewBase
    {
        public FirstPageView()
        {
            this.InitializeComponent();
        }
    }
}

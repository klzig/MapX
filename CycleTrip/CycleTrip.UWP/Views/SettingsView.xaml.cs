using MvvmCross.Uwp.Attributes;

namespace CycleTrip.UWP.Views
{
    [MvxRegionPresentation("ContentFrame")]
    public sealed partial class SettingsView : SettingsViewBase
    {
        public SettingsView()
        {
            this.InitializeComponent();
        }
    }
}

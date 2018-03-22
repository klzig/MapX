using MvvmCross.Uwp.Attributes;

namespace CycleTrip.UWP.Views
{
    [MvxRegionPresentation("ContentFrame")]
    public sealed partial class LocationView : LocationViewBase
    {
        public LocationView()
        {
            this.InitializeComponent();
        }

        private void TextBlock_SelectionChanged(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {

        }
    }
}

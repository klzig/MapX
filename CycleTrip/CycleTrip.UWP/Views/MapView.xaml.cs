using MvvmCross.Uwp.Attributes;

namespace CycleTrip.UWP.Views
{
    [MvxRegionPresentation("ContentFrame")]
    public sealed partial class MapView : MapViewBase
    {
        public MapView()
        {
            this.InitializeComponent();
        }

        private void TextBlock_SelectionChanged(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {

        }
    }
}

using System;
using System.Linq;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Naxam.Controls.Mapbox.Forms;
using Newtonsoft.Json;

using CTForms.Models;
using CTForms.Views;
using CTForms.ViewModels;

namespace CTForms.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MapPage : ContentPage
    {
        public MapPage()
        {
            InitializeComponent();

            map.DidTapOnMapCommand = new Command<Tuple<Position, Point>>((Tuple<Position, Point> obj) =>
            {
                var features = map.GetFeaturesAroundPointFunc.Invoke(obj.Item2, 6, null);
                var filtered = features.Where((arg) => arg.Attributes != null);
                foreach (IFeature feat in filtered)
                {
                    var str = JsonConvert.SerializeObject(feat);
                    System.Diagnostics.Debug.WriteLine(str);
                }

            });
            map.DidFinishLoadingStyleCommand = new Command<MapStyle>((obj) =>
            {
                map.ResetPosition();
                foreach (Layer layer in obj.OriginalLayers)
                {
                    System.Diagnostics.Debug.WriteLine(layer.Id);
                }

            }); 
            map.ZoomLevel = Device.RuntimePlatform == Device.Android ? 4 : 10;
       //     map.Style = "mapbox://styles/klzig/cjflt8g1z09kh2spsbxl3j4z2";
        }

    }
}

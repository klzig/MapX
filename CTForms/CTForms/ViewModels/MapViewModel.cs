using System;
using System.Windows.Input;

using Xamarin.Forms;

using CTForms.Models;
using CTForms.Views;
using CTForms.Properties;

namespace CTForms.ViewModels
{
    public class MapViewModel : BaseViewModel
    {
     //   public Map Item { get; set; }

        public MapViewModel()
        {
            Title = Resources.MenuItemMap;
        }

        Naxam.Controls.Mapbox.Forms.MapStyle currentMapStyle;
        public Naxam.Controls.Mapbox.Forms.MapStyle CurrentMapStyle
        {
            get { return currentMapStyle; }
            set { SetProperty(ref currentMapStyle, value); }
        }

        int zoomLevel = 4;
        public int ZoomLevel
        {
            get { return zoomLevel; }
            set { SetProperty(ref zoomLevel, value); }
        }
    }
}
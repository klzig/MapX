﻿using System;
using System.Windows.Input;

using Xamarin.Forms;

using CTForms.Models;
using CTForms.Views;
using CTForms.Properties;

namespace CTForms.ViewModels
{
    public class MapViewModel : BaseViewModel
    {
        private bool _isMapMode;

        //   public Map Item { get; set; }

        public MapViewModel()
        {
            Title = Resources.MenuItemMap;

            Loc.BeginLocationUpdates();

            ModeToggleCommand = new Command(() => ToggleMode());
        }

        public ICommand ModeToggleCommand { private set; get; }
        public ICommand LocationCommand { private set; get; }
        public ICommand MoreCommand { private set; get; }

        public void ToggleMode()
        {
            // Toggle route mode
            _isMapMode = !_isMapMode;
            if (_isMapMode)
            {
                ModeText = Resources.ViewList;
                ModeIcon = "ic_list_black";
            }
            else
            {
                ModeText = Resources.ViewMap;
                ModeIcon = "ic_map_black";
            }
            ShowMap = _isMapMode;
            ShowList = !_isMapMode;
        }

        private bool panned = false;
        public bool Panned
        {
            get => panned;
            set => SetProperty(ref panned, value);
        }

        private string modeText = Resources.ViewMap;
        public string ModeText
        {
            get => modeText;
            set => SetProperty(ref modeText, value);
        }

        private string modeIcon = "ic_map_black";
        public string ModeIcon
        {
            get => modeIcon;
            set => SetProperty(ref modeIcon, value);
        }

        private bool showMap = false;
        public bool ShowMap
        {
            get => showMap;
            set => SetProperty(ref showMap, value);
        }

        private bool showList = true;
        public bool ShowList
        {
            get => showList;
            set => SetProperty(ref showList, value);
        }

        private string location;
        public string Location
        {
            get => location;
            set => SetProperty(ref location, value);
        }

        private string more;
        public string More
        {
            get => more;
            set => SetProperty(ref more, value);
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
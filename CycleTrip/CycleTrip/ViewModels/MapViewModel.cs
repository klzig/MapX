using MvvmCross.Core.Navigation;
using MvvmCross.Core.ViewModels;
using MvvmCross.Plugins.Messenger;
using CycleTrip.Localization;
using CycleTrip.Messages;

namespace CycleTrip.ViewModels
{
    public class MapViewModel : BaseViewModel
    {
        private readonly IMvxNavigationService _navigationService;
        private readonly IMvxMessenger _messenger;
        private bool _isMapMode;

        public MapViewModel(IMvxNavigationService navigationService, IMvxMessenger messenger)
        {
            _navigationService = navigationService;
            _messenger = messenger;

            _isMapMode = false;
            Mode = AppStrings.Map;
        }

        public override void ViewAppeared()
        {
            var new_title = new ViewTitleMessage(this, AppStrings.Map);
            _messenger.Publish(new_title);
        }

        //public IMvxCommand LocationCommand
        //{
        //    get => new MvxCommand(() => _LocationCommand());
        //}

        public IMvxCommand MoreCommand
        {
            get => new MvxCommand(() => _MoreCommand());
        }

        public void LocationCommand()
        {
            // Location button was pressed
            Panned = false;
        }

        public void PanCommand()
        {
            // Map was panned by user gesture
            Panned = true;
        }

        public bool ModeCommand()
        {
            // Toggle route mode
            _isMapMode = !_isMapMode;
            if (_isMapMode)
            {
                Mode = AppStrings.ViewList;
            }
            else
            {
                Mode = AppStrings.ViewMap;
            }
            return _isMapMode;
        }

        //private void _LocationCommand()
        //{

        //}

        private void _MoreCommand()
        {

        }

        private bool _panned = false;
        public bool Panned
        {
            get => _panned;
            set => SetProperty(ref _panned, value);
        }

        private string _mode;
        public string Mode
        {
            get => _mode;
            set => SetProperty(ref _mode, value);
        }

        private string _location;
        public string Location
        {
            get => _location;
            set => SetProperty(ref _location, value);
        }

        private string _more;
        public string More
        {
            get => _more;
            set => SetProperty(ref _more, value);
        }
    }
}

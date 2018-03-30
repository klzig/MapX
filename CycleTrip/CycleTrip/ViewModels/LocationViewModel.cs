using CycleTrip.Localization;
using CycleTrip.Messages;
using CycleTrip.Services;
using MvvmCross.Core.Navigation;
using MvvmCross.Plugins.Messenger;
using System;

namespace CycleTrip.ViewModels
{
    public class LocationViewModel : BaseViewModel
    {
        private readonly IMvxNavigationService _navigationService;
        private readonly IMvxMessenger _messenger;
        private readonly MvxSubscriptionToken _locationToken;

        public LocationViewModel(IMvxNavigationService navigationService, IMvxMessenger messenger)
        {
            _navigationService = navigationService;
            _messenger = messenger;
            _locationToken = messenger.Subscribe<LocationMessage>(OnLocationMessage);
        }

        public override void ViewAppeared()
        {
            var new_title = new ViewTitleMessage(this, AppStrings.Location);
            _messenger.Publish(new_title);
            _messenger.Publish(new UpdateLocMessage(this));
        }

        private void OnLocationMessage(LocationMessage locationMessage)
        {
            Lat = locationMessage.Lat;
            Lon = locationMessage.Lon;
            Acc = locationMessage.Acc;
            Alt = locationMessage.Alt;
            AltAcc = locationMessage.AltAcc;
            Hdg = locationMessage.Hdg;
            HdgAcc = locationMessage.HdgAcc;
            Spd = string.Format("{0:0.0}", locationMessage.Spd);
            ErrorLbl = locationMessage.ErrorLbl;
            Error = locationMessage.Error;
            Updated = locationMessage.Updated;
        }

        private string _updated;
        public string Updated
        {
            get { return _updated; }
            set { _updated = value; RaisePropertyChanged(() => Updated); }
        }

        private string _errorLbl;
        public string ErrorLbl
        {
            get { return _errorLbl; }
            set { _errorLbl = value; RaisePropertyChanged(() => ErrorLbl); }
        }

        private string _error;
        public string Error
        {
            get { return _error; }
            set { _error = value; RaisePropertyChanged(() => Error); }
        }

        private double _lon;
        public double Lon
        {
            get { return _lon; }
            set { _lon = value; RaisePropertyChanged(() => Lon); }
        }

        private double _lat;
        public double Lat
        {
            get { return _lat; }
            set { _lat = value; RaisePropertyChanged(() => Lat); }
        }

        private double? _acc;
        public double? Acc
        {
            get { return Convert.ToInt32(_acc); }
            set { _acc = value; RaisePropertyChanged(() => Acc); }
        }

        private double? _alt;
        public double? Alt
        {
            get { return Convert.ToInt32(_alt); }
            set { _alt = value; RaisePropertyChanged(() => Alt); }
        }

        private double? _altacc;
        public double? AltAcc
        {
            get { return Convert.ToInt32(_altacc); }
            set { _altacc = value; RaisePropertyChanged(() => AltAcc); }
        }

        private double? _hdg;
        public double? Hdg
        {
            get { return Convert.ToInt32(_hdg); }
            set { _hdg = value; RaisePropertyChanged(() => Hdg); }
        }

        private double? _hdgacc;
        public double? HdgAcc
        {
            get { return Convert.ToInt32(_hdgacc); }
            set { _hdgacc = value; RaisePropertyChanged(() => HdgAcc); }
        }

        private string _spd;
        public string Spd
        {
            get { return _spd; }
            set { _spd = value; RaisePropertyChanged(() => Spd); }
        }
    }
}

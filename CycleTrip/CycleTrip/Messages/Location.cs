using CycleTrip.Localization;
using MvvmCross.Plugins.Location;
using MvvmCross.Plugins.Messenger;

namespace CycleTrip.Messages
{
    public class LocationMessage : MvxMessage
    {
        public LocationMessage(object sender)
        : base(sender)
        {
            Lat = 0;
            Lon = 0;
            Acc = 0;
            Alt = 0;
            AltAcc = 0;
            Hdg = 0;
            HdgAcc = 0;
            Spd = 0;
            ErrorLbl = "";
            Error = "";
            Updated = AppStrings.Never;
        }

        public void Update(double lat, double lon, double? acc = null,
            double? alt = null, double? altacc = null, double? hdg = null, double? hdgacc = null,
            double? spd = null)
        {
            Lat = lat;
            Lon = lon;
            Acc = acc;
            Alt = alt;
            AltAcc = altacc;
            Hdg = hdg;
            HdgAcc = hdgacc;
            Spd = spd;
            ErrorLbl = "";
            Error = "";
            Updated = AppStrings.Now;
        }

        public void UpdateError(string error)
        {
            ErrorLbl = AppStrings.Error;
            Error = error;
        }

        public double Lat { get; private set; }
        public double Lon { get; private set; }
        public double? Acc { get; private set; }
        public double? Alt { get; private set; }
        public double? AltAcc { get; private set; }
        public double? Hdg { get; private set; }
        public double? HdgAcc { get; private set; }
        public double? Spd { get; private set; }
        public string ErrorLbl { get; private set; }
        public string Error { get; private set; }
        public string Updated { get; private set; }
    }

    public class UpdateLocMessage : MvxMessage
    {
        public UpdateLocMessage(object sender) : base(sender)
        {
        }
    }
}

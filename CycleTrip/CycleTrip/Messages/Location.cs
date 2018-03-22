using MvvmCross.Plugins.Location;
using MvvmCross.Plugins.Messenger;

namespace CycleTrip.Messages
{
    public class LocationMessage : MvxMessage
    {
        public LocationMessage(object sender, double lat, double lon, double? acc=null,
            double? alt=null, double? altacc=null, double? hdg=null, double? hdgacc=null, 
            double? spd=null)
        : base(sender)
        {
            Lat = lat;
            Lon = lon;
            Acc = acc;
            Alt = alt;
            AltAcc = altacc;
            Hdg = hdg;
            HdgAcc = hdgacc;
            Spd = spd;
            Error = "";
        }

        public LocationMessage(object sender, string error) : base(sender)
        {
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
        public string Error { get; private set; }
    }
}

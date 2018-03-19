using MvvmCross.Plugins.Location;
using MvvmCross.Plugins.Messenger;

namespace CycleTrip.Messages
{
    public class LocationMessage : MvxMessage
    {
        public double? Accuracy { get; set; }
        public double? Altitude { get; set; }
        public double? AltitudeAccuracy { get; set; }
        public double? Heading { get; set; }
        public double? HeadingAccuracy { get; set; }
        public double? Speed { get; set; }

        public LocationMessage(object sender, double lat, double lon, double? acc=null,
            double? alt=null, double? altacc=null, double? hdg=null, double? hdgacc=null, 
            double? spd=null)
        : base(sender)
        {
            Lat = lat;
            Lon = lon;
            Acc = acc;
            Alt = alt;
            AltAcc = alt;
            Hdg = hdg;
            HdgAcc = hdgacc;
            Spd = spd;
        }

        public double Lat { get; private set; }
        public double Lon { get; private set; }
        public double? Acc { get; private set; }
        public double? Alt { get; private set; }
        public double? AltAcc { get; private set; }
        public double? Hdg { get; private set; }
        public double? HdgAcc { get; private set; }
        public double? Spd { get; private set; }
    }
}

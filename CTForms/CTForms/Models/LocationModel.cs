using System;

namespace CTForms.Models
{  
    public class LocationModel : BaseModel
    {
        private DateTime _timestamp = DateTime.Now;

        public void Update(double lat, double lon, double? acc, double? alt, double? spd, double? hdg, DateTimeOffset gpsTimestamp)
        {
            Lat = lat;
            Lon = lon;
            Acc = acc;
            Alt = alt;
            Spd = spd;
            Hdg = hdg;
            Timestamp = gpsTimestamp;
            Error = "";
            ErrorLbl = "";
            Updated = Properties.Resources.Now;
        }

        public void Update(string error)
        {
            Error = error;
            ErrorLbl = Properties.Resources.Error;

            if (Updated != Properties.Resources.Never)
            {
                TimeSpan elapsed = DateTime.Now - _timestamp;
                _timestamp = DateTime.Now;
                Updated = elapsed.ToString(@"hh\:mm\:ss") + " " + Properties.Resources.Ago;
            }
        }

        private double lat = 0;
        public double Lat
        {
            get => lat;
            set => SetProperty(ref lat, value);
        }

        private double lon = 0;
        public double Lon
        {
            get => lon;
            set => SetProperty(ref lon, value);
        }

        private double? acc = 0;
        public double? Acc
        {
            get => acc;
            set => SetProperty(ref acc, value);
        }

        private double? alt = 0;
        public double? Alt
        {
            get => alt;
            set => SetProperty(ref alt, value);
        }

        private double? hdg = 0;
        public double? Hdg
        {
            get => hdg;
            set => SetProperty(ref hdg, value);
        }

        private double? spd = 0;
        public double? Spd
        {
            get => spd;
            set => SetProperty(ref spd, value);
        }

        private DateTimeOffset timestamp;
        public DateTimeOffset Timestamp
        {
            get => timestamp;
            set => SetProperty(ref timestamp, value);
        }

        private string errorLbl = "";
        public string ErrorLbl
        {
            get => errorLbl;
            set => SetProperty(ref errorLbl, value);
        }

        private string error = "";
        public string Error
        {
            get => error;
            set => SetProperty(ref error, value);
        }

        private string updated = Properties.Resources.Never;
        public string Updated
        {
            get => updated;
            set => SetProperty(ref updated, value);
        }
    }
}

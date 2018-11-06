namespace CycleTrip.Services
{
    // Pre- and Post-build scripts swap in /CycleTrip/Services/Secrets.override for build, if it exists.
    public static class Secrets
    {
        private const string _androidMapboxToken = "your_token_here";
        private const string _iosMapboxToken = "your_token_here";
        private const string _uwpMapboxToken = "your_token_here";
        private const string _uwpMapSDKToken = "your_token_here";

        public static string AndroidMapboxToken => _androidMapboxToken;
        public static string IosMapboxToken => _iosMapboxToken;
        public static string UwpMapboxToken => _uwpMapboxToken;
        public static string UwpMapSDKToken => _uwpMapSDKToken;
    }
}


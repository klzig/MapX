using Xamarin.Essentials;

namespace CTForms.Models
{  
    public class NetworkModel : BaseModel
    {
        private NetworkAccess access;
        public NetworkAccess Access
        {
            get => access;
            set => SetProperty(ref access, value);
        }

        private System.Collections.Generic.IEnumerable<ConnectionProfile> profiles;
        public System.Collections.Generic.IEnumerable<ConnectionProfile> Profiles
        {
            get => profiles;
            set => SetProperty(ref profiles, value);
        }
    }
}

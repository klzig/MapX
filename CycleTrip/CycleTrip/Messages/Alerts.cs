using MvvmCross.Plugins.Messenger;

public enum AlertType
{
    recording,
    notification
}

namespace CycleTrip.Messages
{
    public class AlertMessage : MvxMessage
    {
        public AlertMessage(object sender, AlertType type, bool visible)
        : base(sender)
        {
            Type = type;
            Visible = visible;
        }

        public AlertType Type
        {
            get;
            private set;
        }

        public bool Visible
        {
            get;
            private set;
        }
    }
}

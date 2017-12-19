using MvvmCross.Plugins.Messenger;

namespace CycleTrip.Messages
{
    public class ViewTitleMessage : MvxMessage
    {
        public ViewTitleMessage(object sender, string title)
        : base(sender)
        {
            Title = title;
        }

        // String resource identifier
        public string Title
        {
            get;
            private set;
        }
    }
}

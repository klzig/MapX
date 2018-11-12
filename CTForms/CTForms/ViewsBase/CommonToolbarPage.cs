using Xamarin.Forms;

namespace CTForms.ViewsBase
{
    public class CommonToolbarPage : ContentPage
    {
        public CommonToolbarPage() : base()
        {
            Init();
        }

        private void Init()
        {
            ToolbarItems.Add(new ToolbarItem("Action Name", "notifications", () =>
            {
                // On clicked
                MessagingCenter.Send("Notifications", "Clicked");
            }
            ) { Text = Properties.Resources.Notifications, Priority = 0, Order = ToolbarItemOrder.Primary });
        }
    }
}
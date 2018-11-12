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
            ToolbarItems.Add(new ToolbarItem() { Text = Properties.Resources.Notifications, Icon="notifications", Priority = 0, Order = ToolbarItemOrder.Primary });
        }
    }
}
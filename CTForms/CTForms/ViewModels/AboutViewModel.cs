using System;
using System.Windows.Input;

using Xamarin.Forms;

using CTForms.Properties;

namespace CTForms.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        public AboutViewModel()
        {
            Title = Resources.MenuItemAbout;

            OpenWebCommand = new Command(() => Device.OpenUri(new Uri("https://xamarin.com/platform")));
        }

        public ICommand OpenWebCommand { get; }
    }
}
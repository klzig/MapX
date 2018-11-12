using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using CTForms.ViewsBase;

namespace CTForms.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AboutPage : CommonToolbarPage
    {
        public AboutPage()
        {
            InitializeComponent();
        }
    }
}
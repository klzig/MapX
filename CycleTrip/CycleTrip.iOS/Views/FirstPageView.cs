using MvvmCross.iOS.Views;
using MvvmCross.iOS.Views.Presenters.Attributes;
using CycleTrip.ViewModels;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Localization;

namespace CycleTrip.iOS.Views
{
    [MvxRootPresentation(WrapInNavigationController = true)]
    public partial class FirstPageView : MvxViewController<FirstPageViewModel>
    {
        public FirstPageView() : base("FirstPageView", null)
        {
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();

            // Release any cached data, images, etc that aren't in use.
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            // Perform any additional setup after loading the view, typically from a nib.
            var set = this.CreateBindingSet<FirstPageView, FirstPageViewModel>();
            set.Bind(Button).For("Title").To(ViewModel => ViewModel.SharedSource).WithConversion(new MvxLanguageConverter(), "SecondPage");
            set.Bind(Button).To(vm => vm.SecondPageCommand);
            set.Apply();
        }
    }
}
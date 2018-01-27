using MvvmCross.iOS.Views;
using MvvmCross.iOS.Views.Presenters.Attributes;
using CycleTrip.ViewModels;
using MvvmCross.Binding.BindingContext;

namespace CycleTrip.iOS.Views
{
    [MvxRootPresentation(WrapInNavigationController = false)]
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
            set.Bind(Button).To(vm => vm.SecondPageCommand);
            set.Apply();
        }
    }
}
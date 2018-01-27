using MvvmCross.Binding.BindingContext;
using MvvmCross.iOS.Views;
using MvvmCross.iOS.Views.Presenters.Attributes;
using CycleTrip.ViewModels;

namespace CycleTrip.iOS.Views
{
    [MvxRootPresentation(WrapInNavigationController = true)]
    public partial class MainView : MvxViewController<MainViewModel>
    {
        public MainView() : base("MainView", null)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            var set = this.CreateBindingSet<MainView, MainViewModel>();
            set.Bind(TextField).To(vm => vm.Text);
            set.Bind(Button).To(vm => vm.ResetTextCommand);
            set.Bind(FirstPageButton).To(vm => vm.FirstPageCommand);
            set.Bind(SecondPageButton).To(vm => vm.SecondPageCommand);
            set.Bind(InfoButton).To(vm => vm.InfoCommand);
            set.Bind(SettingsButton).To(vm => vm.SettingsCommand);
            set.Apply();
        }
    }
}

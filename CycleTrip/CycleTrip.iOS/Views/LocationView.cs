using MvvmCross.iOS.Views;
using MvvmCross.iOS.Views.Presenters.Attributes;
using CycleTrip.ViewModels;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Localization;

namespace CycleTrip.iOS.Views
{
    [MvxRootPresentation(WrapInNavigationController = true)]
    public partial class LocationView : MvxViewController<LocationViewModel>
    {
        public LocationView() : base("LocationView", null)
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

            var set = this.CreateBindingSet<LocationView, LocationViewModel>();
            set.Bind(UpdatedLbl).To(ViewModel => ViewModel.SharedSource).WithConversion(new MvxLanguageConverter(), "Updated");
            set.Bind(Updated).To(vm => vm.Updated);
            set.Bind(ErrorLbl).To(vm => vm.ErrorLbl);
            set.Bind(Error).To(vm => vm.Error);
            set.Bind(LatLbl).To(ViewModel => ViewModel.SharedSource).WithConversion(new MvxLanguageConverter(), "Latitude");
            set.Bind(Lat).To(vm => vm.Lat);
            set.Bind(LonLbl).To(ViewModel => ViewModel.SharedSource).WithConversion(new MvxLanguageConverter(), "Longitude");
            set.Bind(Lon).To(vm => vm.Lon);
            set.Bind(AccLbl).To(ViewModel => ViewModel.SharedSource).WithConversion(new MvxLanguageConverter(), "Accuracy");
            set.Bind(Acc).To(vm => vm.Acc);
            set.Bind(AltLbl).To(ViewModel => ViewModel.SharedSource).WithConversion(new MvxLanguageConverter(), "Altitude");
            set.Bind(Alt).To(vm => vm.Alt);
            set.Bind(AltAccLbl).To(ViewModel => ViewModel.SharedSource).WithConversion(new MvxLanguageConverter(), "Accuracy");
            set.Bind(AltAcc).To(vm => vm.AltAcc);
            set.Bind(HdgLbl).To(ViewModel => ViewModel.SharedSource).WithConversion(new MvxLanguageConverter(), "Heading");
            set.Bind(Hdg).To(vm => vm.Hdg);
            set.Bind(HdgAccLbl).To(ViewModel => ViewModel.SharedSource).WithConversion(new MvxLanguageConverter(), "Accuracy");
            set.Bind(HdgAcc).To(vm => vm.HdgAcc);
            set.Bind(SpdLbl).To(ViewModel => ViewModel.SharedSource).WithConversion(new MvxLanguageConverter(), "Speed");
            set.Bind(Spd).To(vm => vm.Spd);
            set.Apply();
        }
    }
}
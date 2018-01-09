using CycleTrip.ViewModels;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using MvvmCross.Plugins.Messenger;

namespace CycleTrip
{
    public class App : MvxApplication
    {
        public override void Initialize()
        {
            base.Initialize();

            Mvx.LazyConstructAndRegisterSingleton<IMvxMessenger, MvxMessengerHub>();
            //           CreatableTypes()
            //               .EndingWith("Service")
            //               .AsInterfaces()
            //               .RegisterAsLazySingleton();

            RegisterNavigationServiceAppStart<MainViewModel>();
        }
    }
}

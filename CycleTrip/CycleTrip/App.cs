//using MvvmCross.Platform.IoC;
using CycleTrip.ViewModels;
using MvvmCross.Platform;
using MvvmCross.Plugins.Messenger;

namespace CycleTrip
{
    public class App : MvvmCross.Core.ViewModels.MvxApplication
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

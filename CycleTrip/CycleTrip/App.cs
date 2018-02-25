using CycleTrip.Localization;
using CycleTrip.ViewModels;
using MvvmCross.Core.ViewModels;
using MvvmCross.Localization;
using MvvmCross.Platform;
using MvvmCross.Plugins.Messenger;
using MvvmCross.Plugins.ResxLocalization;

namespace CycleTrip
{
    public class App : MvxApplication
    {
        public override void Initialize()
        {
            base.Initialize();

            Mvx.LazyConstructAndRegisterSingleton<IMvxMessenger, MvxMessengerHub>();
            Mvx.RegisterSingleton<IMvxTextProvider>(new MvxResxTextProvider(AppStrings.ResourceManager));
 
            //           CreatableTypes()
            //               .EndingWith("Service")
            //               .AsInterfaces()
            //               .RegisterAsLazySingleton();

            RegisterNavigationServiceAppStart<MainViewModel>();
        }
    }
}

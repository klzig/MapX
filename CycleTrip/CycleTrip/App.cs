//using MvvmCross.Platform.IoC;
using CycleTrip.ViewModels;

namespace CycleTrip
{
    public class App : MvvmCross.Core.ViewModels.MvxApplication
    {
        public override void Initialize()
        {
             base.Initialize();
 //           CreatableTypes()
 //               .EndingWith("Service")
 //               .AsInterfaces()
 //               .RegisterAsLazySingleton();

            RegisterNavigationServiceAppStart<MainViewModel>();
        }
    }
}

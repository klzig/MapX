using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CycleTrip.UWP.Views;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform.Logging;
using MvvmCross.Platform.Platform;
using MvvmCross.Uwp.Platform;
using Windows.UI.Xaml.Controls;

namespace CycleTrip.UWP
{
    public class Setup : MvxWindowsSetup
    {
        public Setup(Frame rootFrame) : base(rootFrame)
        {
        }

        protected override IMvxApplication CreateApp()
        {
            return new CycleTrip.App();
        }

        protected override IMvxTrace CreateDebugTrace()
        {
            return new DebugTrace();
        }

        protected override void InitializeFirstChance()
        {
            base.InitializeFirstChance();

 //           Mvx.RegisterSingleton<IDialogService>(() => new DialogService());
 //           Mvx.RegisterSingleton<MvxPresentationHint>(() => new MvxPanelPopToRootPresentationHint());
        }

        // Fixes crash on launch: https://github.com/MvvmCross/MvvmCross/issues/2333
        protected override MvxLogProviderType GetDefaultLogProviderType() => MvxLogProviderType.None;
    }
}

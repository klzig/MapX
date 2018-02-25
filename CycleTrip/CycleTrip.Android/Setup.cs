using System.Collections.Generic;
using System.Reflection;
using Android.Content;
using MvvmCross.Droid.Platform;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform.Platform;
using MvvmCross.Droid.Views;
using MvvmCross.Droid.Support.V7.AppCompat;
using MvvmCross.Binding.Bindings.Target.Construction;
using CycleTrip.Droid.Presenters;
using MvvmCross.Platform;
using CycleTrip.PresentationHints;
using MvvmCross.Localization;

// http://dailydotnettips.com/2016/05/11/accessing-platform-specific-code-using-ioc-in-mvvm-cross/recordingMessage

namespace CycleTrip.Droid
{
    public class Setup : MvxAppCompatSetup
    {
        public Setup(Context applicationContext) : base(applicationContext)
        {
        }

        protected override IMvxApplication CreateApp()
        {
            return new App();
        }

        protected override IMvxTrace CreateDebugTrace()
        {
            return new DebugTrace();
        }

        protected override IEnumerable<Assembly> AndroidViewAssemblies => new List<Assembly>(base.AndroidViewAssemblies)
        {
            typeof(global::Android.Support.V7.Widget.Toolbar).Assembly,
        };
   
        protected override IMvxAndroidViewPresenter CreateViewPresenter()
        {
            var presenter = new MvxAppCompatViewPresenter(AndroidViewAssemblies);
            presenter.AddPresentationHintHandler<ClearBackstackHint>((new BackStackHintHandler()).HandleClearBackstackHint);
            Mvx.RegisterSingleton<IMvxAndroidViewPresenter>(presenter);
            return presenter;
        }

        protected override void FillValueConverters(MvvmCross.Platform.Converters.IMvxValueConverterRegistry registry)
        {
            base.FillValueConverters(registry);
            registry.AddOrOverwrite("Language", new MvxLanguageConverter());
        }
    }
}

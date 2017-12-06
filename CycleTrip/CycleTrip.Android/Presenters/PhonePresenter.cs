using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
//using MvvmCross.Droid.Support.V7.AppCompat;
using System.Reflection;
using MvvmCross.Core.ViewModels;
using MvvmCross.Droid.Support.V7.AppCompat;

// This isn't currently used.  To use it, reference it instead of MvxAppCompatViewPresenter in setup.cs.
namespace CycleTrip.Droid.Presenters
{
    class PhonePresenter: MvxAppCompatViewPresenter
    {
        public PhonePresenter(IEnumerable<Assembly> androidViewAssemblies) : base(androidViewAssemblies)
        {
        }

        public override void Show(MvxViewModelRequest request)
        {
            if (request.PresentationValues?["NavigationCommand"] == "StackClear")
            {
                CurrentFragmentManager.PopBackStack();
            }
            base.Show(request);
        }
    }
}
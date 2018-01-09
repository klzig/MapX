using CycleTrip.ViewModels;
using MvvmCross.Core.ViewModels;
using MvvmCross.Uwp.Views;

namespace CycleTrip.UWP.Views
{
    /// <summary>
    /// Work-around for lack of XAML support for generic types
    /// </summary>
    public abstract class MainViewBase : MvxWindowsPage<MainViewModel>
    {
    }

    /// <summary>
    /// Work-around for lack of XAML support for generic types
    /// </summary>
    public abstract class FirstPageViewBase : MvxWindowsPage<FirstPageViewModel>
    {
    }

    /// <summary>
    /// Work-around for lack of XAML support for generic types
    /// </summary>
    public abstract class SecondPageViewBase : MvxWindowsPage<SecondPageViewModel>
    {
    }

    /// <summary>
    /// Work-around for lack of XAML support for generic types
    /// </summary>
    public abstract class InfoViewBase : MvxWindowsPage<InfoViewModel>
    {
    }

    /// <summary>
    /// Work-around for lack of XAML support for generic types
    /// </summary>
    public abstract class SettingsViewBase : MvxWindowsPage<SettingsViewModel>
    {
    }
}

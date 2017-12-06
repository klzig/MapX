using System;
using System.Collections.Generic;
using MvvmCross.Core.ViewModels;
using MvvmCross.Core.Navigation;
using CycleTrip.PresentationHints;

namespace CycleTrip.ViewModels
{
    public class MainViewModel : MvxViewModel
    {
        readonly Type[] _menuItemTypes = {
            typeof(FirstPageViewModel),
            typeof(SettingsViewModel),
        };
        public IEnumerable<string> MenuItems { get; private set; } = new[] { "First Page", "Settings" };
   
        private readonly IMvxNavigationService _navigationService;

        public MainViewModel(IMvxNavigationService navigationService)
        {
            _navigationService = navigationService;
        }
    
        public void ShowDefaultMenuItemAsync()
        {
            NavigateTo(0);
        }

        public void NavigateTo(int position)
        {
            Type vm = _menuItemTypes[position];
            ChangePresentation(new ClearBackstackHint());
//           var presentationBundle = new MvxBundle(new Dictionary<string, string> { { "NavigationMode", "ClearStack" } });
            _navigationService.Navigate(vm, null);  // Fragment's OnCreateView not called unless second parameter is null

        }

  //      public async Task NavigateTo(int position)
  //      {
  //          Type vm = _menuItemTypes[position];
  //          var presentationBundle = new MvxBundle(new Dictionary<string, string> { {"NavigationMode", "ClearStack"} });
  //          await _navigationService.Navigate(vm, null);
  //      }

        public IMvxCommand ResetTextCommand => new MvxCommand(ResetText);
        private void ResetText()
        {
            Text = "Hello MvvmCross";
        }

        private string _text = "Hello MvvmCross";
        public string Text
        {
            get { return _text; }
            set { SetProperty(ref _text, value); }
        }
    }

    public class MenuItem : Tuple<string, Type>
    {
        public MenuItem(string displayName, Type viewModelType)
            : base(displayName, viewModelType)
        { }

        public string DisplayName
        {
            get { return Item1; }
        }

        public Type ViewModelType
        {
            get { return Item2; }
        }
    }
}

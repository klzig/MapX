using Xamarin.Forms;
using CTForms.Models;
using CTForms.Services;
 
namespace CTForms.ViewModels
{
    public class BaseViewModel : BaseModel
    {
        public IDataStore<Item> DataStore => DependencyService.Get<IDataStore<Item>>() ?? new MockDataStore();
 //       public ILocationService Loc => DependencyService.Get<ILocationService>() ?? new LocationService();

        public BaseViewModel()
        {
        }

        bool isBusy = false;
        public bool IsBusy
        {
            get { return isBusy; }
            set { SetProperty(ref isBusy, value); }
        }

        string title = string.Empty;
        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }
    }
}

using System.Collections.Generic;
using System.Linq;
using Mobile.Models;

using Mobile.ViewModels.Cars;
using Mobile.ViewModels.Cars.Interfaces;
using Mobile.ViewModels.Core;
using Mobile.ViewModels.Trucks;
using Mobile.ViewModels.Trucks.Interfaces;
using Mobile.Views;
using Mobile.Views.Cars;
using Mobile.Views.Trucks;
using Xamarin.Forms;

namespace Mobile.ViewModels
{
    public interface IMainPageViewModel
    {
        CarListViewModel CarListViewModel { get; set; }
        TruckListViewModel TruckListViewModel { get; set; }
    }
    public class MainPageViewModel : BindableBase, IMainPageViewModel
    {
        private HomeMenuItem _selectedItem;
        public List<HomeMenuItem> MenuItems { get; set; }
        public CarListViewModel CarListViewModel { get; set; }
        public TruckListViewModel TruckListViewModel { get; set; }


        public HomeMenuItem SelectedItem
        {
            set
            {
                SetProperty(ref _selectedItem, value);
                if (value == null) return;
                var page = Application.Current.MainPage as NavigationPage;
                switch (value.Title)
                {
                    case "Car":
                        
                        var carPage = new CarListView
                        {
                            BindingContext = CarListViewModel,
                        };
                        
                        page?.PushAsync(carPage,true);
                        //page.po
                        
                        //if (page != null) page.Popped += Page_Popped; 
                        CarListViewModel.RefreshCarListCommand.Execute(null);
                        SelectedItem = null;
                        break;
                    case "Truck":
                        var truckPage = new TruckListView
                        {
                            BindingContext = TruckListViewModel,
                        };

                        page?.PushAsync(truckPage, true);
                        //page.po

                        //if (page != null) page.Popped += Page_Popped; 
                        TruckListViewModel.RefreshTruckListCommand.Execute(null);
                        SelectedItem = null;
                        break;
                }
            }
            get => _selectedItem;
        }

        private void Page_PopRequested(object sender, Xamarin.Forms.Internals.NavigationRequestedEventArgs e)
        {
            //throw new System.NotImplementedException();
            //var page = Application.Current.MainPage as NavigationPage;
            
            //((CarListPage)sender).BindingContextChanged
        }

        //private void Page_Popped(object sender, NavigationEventArgs e)
        //{
        //    SelectedItem = null;
        //    ((NavigationPage)sender).Popped -= Page_Popped;
        //}

        public MainPageViewModel(ICarListViewModel carListViewModel,ITruckListViewModel truckListViewModel)
        {
            CarListViewModel = (CarListViewModel) carListViewModel;
            TruckListViewModel = (TruckListViewModel) truckListViewModel;
            MenuItems = new List<HomeMenuItem>
            {
                new HomeMenuItem {Id = MenuItemType.Car, Title="Car" },
                new HomeMenuItem {Id = MenuItemType.Truck, Title="Truck" }
            };
        }

        //private void Page_Popped(object sender, NavigationEventArgs e)
        //{
        //    

        //    ((NavigationPage)sender).Popped -= Page_Popped;
        //}

    }

}
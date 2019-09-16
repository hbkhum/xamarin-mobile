using Microsoft.Extensions.DependencyInjection;
using Mobile.Models;
using Mobile.Services;
using Mobile.Services.Interface;
using Mobile.ViewModels.Cars;
using Mobile.ViewModels.Cars.Interfaces;
using Mobile.ViewModels.Trucks;
using Mobile.ViewModels.Trucks.Interfaces;

namespace Mobile.ViewModels
{
    public class ViewModelLocator
    {
        private MainPageViewModel _mainPageViewModel;
        public MainPageViewModel MainPageViewModelStatic
        {
            get
            {

                if (_mainPageViewModel != null) return _mainPageViewModel;
                var serviceProvider = ServiceCollection();

                _mainPageViewModel = (MainPageViewModel)serviceProvider.GetService<IMainPageViewModel>(); //new ();
                return _mainPageViewModel;
            }
        }

        private static ServiceProvider ServiceCollection()
        {
            var serviceProvider = new ServiceCollection()

                .AddScoped(typeof(IClientService<>), typeof(ClientService<>))

                .AddSingleton<ICarListViewModel, CarListViewModel>()
                .AddSingleton<ICarViewModel, CarViewModel>()
                .AddSingleton<ITruckListViewModel, TruckListViewModel>()
                .AddSingleton<ITruckViewModel, TruckViewModel>()
                .AddSingleton<IMainPageViewModel, MainPageViewModel>()

                .AddSingleton<IDataService, DataService>()
                .AddSingleton<ICarService, CarService>()
                .AddSingleton<ITruckService, TruckService>()



                .BuildServiceProvider();
            //var l = serviceProvider.GetServices(typeof(IClientService<>));
            return serviceProvider;
        }
    }


}
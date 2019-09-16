using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.AspNetCore.SignalR.Client;
using Mobile.Core;
using Mobile.Models;
using Mobile.Services;
using Mobile.ViewModels.Cars.Interfaces;
using Mobile.ViewModels.Core;
using Mobile.Views;
using Mobile.Views.Cars;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace Mobile.ViewModels.Cars
{
    public class CarListViewModel: ValidatableBindableBase, ICarListViewModel
    {
        private IEnumerable<Car> _carList;
        private readonly IDataService _dataService;
        private Car _selectedCar;
        private CarViewModel _carViewModel;

        public IEnumerable<Car> CarList
        {
            get => _carList?.Where(c => c.Make.ToLower().Contains("")) ?? SetProperty(ref _carList, null);
            set => SetProperty(ref _carList, value);
        }

        public Car SelectedCar
        {
            set
            {

                SetProperty(ref _selectedCar, value);
                if (value == null && _selectedCar == null) return;

                _carViewModel.Car = _selectedCar.Clone() as Car;

                var page = Application.Current.MainPage as NavigationPage;
                var carPageDetail = new CarDetailView()
                {
                    BindingContext = _carViewModel
                };
                page?.PushAsync(carPageDetail);
                SelectedCar = null;
                //if (page != null) page.PopRequested += Page_PopRequested; 
                //(object sender, NavigationEventArgs e) =>
                //{
                //    SelectedCar = null;

                //    /*var navpage = e.Page as CarListPage;
                //    if (navpage != null)
                //    {
                //        navpage.CleanupPage();
                //    }
                //    e.Page.BindingContext = null; */
                //};


                /*if (_selectedCar != value && _selectedCar == null)
                {
                    _selectedCar = value;
                }
                else
                {

                    if (_selectedCar != null && _selectedCar != value)
                    {
                        _selectedCar = value;
                        if (_selectedCar == null)
                        {
                            SetProperty(ref _selectedCar, value);
                            if (value == null) return;
                        }
                    }
                }

                if (_selectedCar != null)
                {
                    var selectedEmployee = _selectedCar.Clone() as Car;
                    CarViewModel.Car = SelectedCar;
                }

                SetProperty(ref _selectedCar, value);
                var page = Application.Current.MainPage as NavigationPage;
                var carPageDetail = new CarDetailPage()
                {
                    BindingContext = SelectedCar
                };
                page?.PushAsync(carPageDetail);
                if (page != null) page.Popped += Page_Popped;*/
            }
            get => _selectedCar;
        }



        public ICommand RefreshCarListCommand { get; }
        public ICommand AddCarCommand { get; }

        public CarListViewModel(IDataService dataService)
        {
            _dataService = dataService;
            RefreshCarListCommand = new RelayCommand(async delegate { await Refresh(); });
            AddCarCommand = new RelayCommand(p => Add());
            _carViewModel = new CarViewModel(_dataService);
            _carViewModel.CarEvent += _carViewModel_CarEvent;
            SignalRNetCore();
            //_carViewModel = (CarViewModel) carViewModel;
        }

        private async Task _carViewModel_CarEvent(Car car)
        {
            await Task.Run(() =>
            {
                var row = CarList.FirstOrDefault(c => c.CarId == car.CarId);
                if (row != null)
                {
                    row.Make = car.Make;
                    row.Model = car.Model;
                    row.VIN = car.VIN;
                    row.Color = car.Color;
                    row.Year = car.Year;

                }
                else
                {
                    var list = CarList.ToList();
                    list.Add(car);
                    CarList = list.OrderBy(c => c.Year);
                }
                NotifyPropertyChanged("CarList");
            });
        }

        private void Add()
        {
            //SelectedCar = null;
            SelectedCar = new Car();
            //if (_carViewModel != null) _carViewModel.Car = _selectedCar;
        }

        private async Task Refresh()
        {
            CarList = await _dataService.CarService.GetAllCars("", "");

        }

        private void SignalRNetCore()
        {
            try
            {

                var car = new HubConnectionBuilder()
                    .WithUrl("http://192.168.2.200:5000/hubs/car")
                    .Build();
                //testGroupHub.On<object>("UpdateTestPlan", UpdateTestPlan);
                car.On<object>("AddCar", AddCar);
                car.On<object>("UpdateCar", UpdateCar);
                //testGroupHub.On<object>("DeleteTestPlan", DeleteTestPlan);
                //// Start the connection
                Task.Run(async () =>
                {
                    await car.StartAsync();
                    Console.WriteLine("conecyado");
                });

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        private void UpdateCar(object obj)
        {
            var car = JsonConvert.DeserializeObject<Car>(obj.ToString());
            var row = CarList.First(c => c.CarId == car.CarId);
            var comparer = new ObjectsComparer.Comparer<Car>();
            var k = comparer.Compare(car, row, out _);
            if (k) return;
            row.Make = car.Make;
            row.Model = car.Model;
            row.VIN = car.VIN;
            row.Color = car.Color;
            row.Year = car.Year;
            NotifyPropertyChanged("CarList");
        }

        private void AddCar(object obj)
        {
            var car = JsonConvert.DeserializeObject<Car>(obj.ToString());
            var row = CarList.FirstOrDefault(c => c.CarId == car.CarId);
            if (row == null)
            {
                var list = CarList.ToList();
                list.Add(car);
                CarList = list.OrderBy(c => c.Year);
            }
            NotifyPropertyChanged("CarList");
        }
    }
}
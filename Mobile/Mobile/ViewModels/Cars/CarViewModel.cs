using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using Mobile.Core;
using Mobile.Helper;
using Mobile.Models;
using Mobile.Services;
using Mobile.ViewModels.Cars.Interfaces;
using Mobile.ViewModels.Core;
using Xamarin.Forms;

namespace Mobile.ViewModels.Cars
{
    public class CarViewModel: ValidatableBindableBase, ICarViewModel
    {



        public delegate Task CarHandler(Car car);
        public event CarHandler CarEvent;

        private Car _car;
        private Car _carClone;
        private readonly IDataService _dataService;

        public RelayCommand UpdateCommand { get; }
        public RelayCommand CancelCommand { get; }

        public bool CanSave
        {
            get
            {
                return _car != null && (!_car.HasErrors && _car.IsDirty);
            }
        }

        public Car Car
        {
            set
            {
                SetProperty(ref _car, value);
                _carClone = _car.Clone() as Car;
            }
            get => _car;
        }



        public CarViewModel(IDataService dataService)
        {
            _dataService = dataService;
            UpdateCommand = new RelayCommand(async delegate { await Update(); }, n => true);
            CancelCommand = new RelayCommand(async delegate { await Cancel(); }, n =>true);

        }

        private async Task Cancel()
        {
            var page = Application.Current.MainPage as NavigationPage;
            if (Car.CarId != Guid.Empty)
            {
                Car = _carClone;
                ValidationHelper.IsFormValid(Car, page?.CurrentPage);
            }
            else
            {
                page?.CurrentPage.Navigation.PopAsync(true);
            }
            


            //if (!ValidationHelper.IsFormValid(Car, page.CurrentPage)) { return; }
            await Task.Run(() =>
            {

                //_carClone = null;
            });
        }

        private async Task Update()
        {
            var page = Application.Current.MainPage as NavigationPage;
            if (!ValidationHelper.IsFormValid(Car, page.CurrentPage)) { return; }
            
            var flag = false;
            if (_car.CarId != Guid.Empty)
            {
                //var controller = await _metroWindow.ShowProgressAsync("Employee", "Employee Updated Successfully");
                flag = await _dataService.CarService.UpdateCar(_car);
            }
            else
            {
                //var controller = await _metroWindow.ShowProgressAsync("Employee", "Employee Added Successfully");
                flag = await _dataService.CarService.CreateCar(_car);
            }

            if (flag)
            {
                await OnCarEvent(Car);
                page?.CurrentPage.Navigation.PopAsync(true);
            }
            await page.DisplayAlert("Success", "Validation Success!", "OK");
        }
        protected virtual async Task OnCarEvent(Car car)
        {
            await Task.Run(() => CarEvent?.Invoke(car));
        }
    }
}

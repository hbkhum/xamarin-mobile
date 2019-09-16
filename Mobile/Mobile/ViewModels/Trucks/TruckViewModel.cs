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
using Mobile.ViewModels.Trucks.Interfaces;
using Xamarin.Forms;

namespace Mobile.ViewModels.Trucks
{
    public class TruckViewModel : ValidatableBindableBase, ITruckViewModel
    {



        public delegate Task TruckHandler(Truck truck);
        public event TruckHandler TruckEvent;

        private Truck _truck;
        private Truck _truckClone;
        private readonly IDataService _dataService;

        public RelayCommand UpdateCommand { get; }
        public RelayCommand CancelCommand { get; }

        public bool CanSave
        {
            get
            {
                return _truck != null && (!_truck.HasErrors && _truck.IsDirty);
            }
        }

        public Truck Truck
        {
            set
            {
                SetProperty(ref _truck, value);
                _truckClone = _truck.Clone() as Truck;
            }
            get => _truck;
        }



        public TruckViewModel(IDataService dataService)
        {
            _dataService = dataService;
            UpdateCommand = new RelayCommand(async delegate { await Update(); }, n => true);
            CancelCommand = new RelayCommand(async delegate { await Cancel(); }, n => true);

        }

        private async Task Cancel()
        {
            var page = Application.Current.MainPage as NavigationPage;
            if (Truck.TruckId != Guid.Empty)
            {
                Truck = _truckClone;
                ValidationHelper.IsFormValid(Truck, page?.CurrentPage);
            }
            else
            {
                page?.CurrentPage.Navigation.PopAsync(true);
            }



            //if (!ValidationHelper.IsFormValid(Truck, page.CurrentPage)) { return; }
            await Task.Run(() =>
            {

                //_truckClone = null;
            });
        }

        private async Task Update()
        {
            var page = Application.Current.MainPage as NavigationPage;
            if (!ValidationHelper.IsFormValid(Truck, page.CurrentPage)) { return; }

            var flag = false;
            if (_truck.TruckId != Guid.Empty)
            {
                //var controller = await _metroWindow.ShowProgressAsync("Employee", "Employee Updated Successfully");
                flag = await _dataService.TruckService.UpdateTruck(_truck);
            }
            else
            {
                //var controller = await _metroWindow.ShowProgressAsync("Employee", "Employee Added Successfully");
                flag = await _dataService.TruckService.CreateTruck(_truck);
            }

            if (flag)
            {
                await OnTruckEvent(Truck);
                page?.CurrentPage.Navigation.PopAsync(true);
            }
            await page.DisplayAlert("Success", "Validation Success!", "OK");
        }
        protected virtual async Task OnTruckEvent(Truck truck)
        {
            await Task.Run(() => TruckEvent?.Invoke(truck));
        }
    }
}

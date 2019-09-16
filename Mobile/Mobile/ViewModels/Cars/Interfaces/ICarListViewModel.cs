using System.Collections.Generic;
using System.Windows.Input;
using Mobile.Models;

namespace Mobile.ViewModels.Cars.Interfaces
{
    public interface ICarListViewModel
    {
        IEnumerable<Models.Car> CarList { get; }
        Car SelectedCar { get; }
        ICommand RefreshCarListCommand { get; }
        ICommand AddCarCommand { get; }
    }
}
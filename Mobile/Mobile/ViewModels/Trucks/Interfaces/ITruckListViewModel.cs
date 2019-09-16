using System.Collections.Generic;
using System.Windows.Input;
using Mobile.Models;

namespace Mobile.ViewModels.Trucks.Interfaces
{
    public interface ITruckListViewModel
    {
        IEnumerable<Truck> TruckList { get; }
        Truck SelectedTruck { get; }
        ICommand RefreshTruckListCommand { get; }
        ICommand AddTruckCommand { get; }
    }
}
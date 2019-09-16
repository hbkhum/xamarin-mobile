using Mobile.Core;
using Mobile.Models;

namespace Mobile.ViewModels.Trucks.Interfaces
{
    public interface ITruckViewModel
    {
        Truck Truck { get; }
        RelayCommand UpdateCommand { get; }
        RelayCommand CancelCommand { get; }
    }
}
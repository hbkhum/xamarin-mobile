using Mobile.Core;
using Mobile.Models;

namespace Mobile.ViewModels.Cars.Interfaces
{
    public interface ICarViewModel
    {
        Car Car { get; }
        RelayCommand UpdateCommand { get; }
        RelayCommand CancelCommand { get; }
    }
}
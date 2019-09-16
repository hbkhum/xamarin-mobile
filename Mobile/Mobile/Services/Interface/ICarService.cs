using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Mobile.Models;

namespace Mobile.Services.Interface
{
    public interface ICarService
    {
        Task<bool> CreateCar(Car entity);
        Task<bool> DeleteCar(Guid id);
        Task<IEnumerable<Car>> GetAllCars(string whereValue, string orderBy);
        Task<bool> UpdateCar(Car entity);
        Task<Car> GetCarByIdAsync(Guid? id);
    }
}
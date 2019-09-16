using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Mobile.Models;

namespace Mobile.Services.Interface
{
    public interface ITruckService
    {
        Task<bool> CreateTruck(Truck entity);
        Task<bool> DeleteTruck(Guid id);
        Task<IEnumerable<Truck>> GetAllTrucks(string whereValue, string orderBy);
        Task<bool> UpdateTruck(Truck entity);
        Task<Truck> GetTruckByIdAsync(Guid? id);
    }
}
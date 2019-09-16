using System;

namespace Mobile.Models
{
    public class Truck : Vehicle
    {
        private Guid _truckId;

        public Guid TruckId
        {
            get => _truckId;
            set => SetProperty(ref _truckId, value);
        }
    }
}
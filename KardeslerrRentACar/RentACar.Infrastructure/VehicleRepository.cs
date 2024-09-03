using RentACar.Domain;
using RentACar.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACar.Infrastructure
{
    public class VehicleRepository : IVehicleRepository
    {
        Task<Vehicle> IVehicleRepository.AddVehicleAsync(Vehicle vehicle)
        {
            throw new NotImplementedException();
        }

        Task<bool> IVehicleRepository.DeleteVehicleAsync(int vehicleId)
        {
            throw new NotImplementedException();
        }

        Task<Vehicle> IVehicleRepository.GetVehicleDetailsAsync(int vehicleId)
        {
            throw new NotImplementedException();
        }

        Task<List<Vehicle>> IVehicleRepository.GetVehiclesAsync()
        {
            throw new NotImplementedException();
        }

        Task<Vehicle> IVehicleRepository.UpdateVehicle(int vehicleId, Vehicle vehicle)
        {
            throw new NotImplementedException();
        }
    }
}

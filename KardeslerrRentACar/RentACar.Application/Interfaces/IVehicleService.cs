using System;
using RentACar.Domain;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RentACar.DTOs.Vehicle;

namespace RentACar.Application.Interfaces
{
    public interface IVehicleService
    {
        Task<List<Vehicle>> GetVehiclesAsync();
        Task<Vehicle> GetVehicleDetailsAsync(int vehicleId);

        Task<VehicleDTO> AddVehicleAsync (VehicleDTO vehicleDTO);

        Task<bool> DeleteVehicleAsync(int vehicleId);

        Task<Vehicle> UpdateVehicleAsync(int vehicleId, Vehicle vehicle);



    }
}

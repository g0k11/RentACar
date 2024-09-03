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
        Task<List<Vehicle>> GetVehicles();
        Task<Vehicle> GetVehicleDetails(int vehicleId);

        Task<AddVehicleDTO> AddVehicle (AddVehicleDTO vehicleDTO);

        Task<bool> DeleteVehicle(int vehicleId);

        Task<Vehicle> UpdateVehicle(int vehicleId, Vehicle vehicle);



    }
}

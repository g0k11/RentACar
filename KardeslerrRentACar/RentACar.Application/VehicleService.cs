using RentACar.Application.Interfaces;
using RentACar.Domain;
using RentACar.DTOs.Vehicle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RentACar.Infrastructure.Interfaces;

namespace RentACar.Application
{
    public class VehicleService : IVehicleService
    {
        private readonly IVehicleRepository _vehicleRepository;
        public VehicleService(IVehicleRepository vehicleRepository) { _vehicleRepository = vehicleRepository; }

        public Task<AddVehicleDTO> AddVehicle(AddVehicleDTO vehicleDTO)
        {
            Vehicle vehicle = new Vehicle() {
                LicensePlate = vehicleDTO.LicensePlate,
                Brand = vehicleDTO.Brand,
                Color = vehicleDTO.Color,
                FuelType = vehicleDTO.FuelType,
                Hp = vehicleDTO.Hp,
                VehicleType = vehicleDTO.VehicleType,
                Year = vehicleDTO.Year,
                Kms = vehicleDTO.Kms,
                RentalPrice = vehicleDTO.RentalPrice,
                Status = "Available"
                };

        }

        public Task<bool> DeleteVehicle(int vehicleId)
        {
            throw new NotImplementedException();
        }

        public Task<Vehicle> GetVehicleDetails(int vehicleId)
        {
            throw new NotImplementedException();
        }

        public Task<List<Vehicle>> GetVehicles()
        {
            throw new NotImplementedException();
        }

        public Task<Vehicle> UpdateVehicle(int vehicleId, Vehicle vehicle)
        {
            throw new NotImplementedException();
        }
    }
}

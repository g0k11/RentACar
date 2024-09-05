using RentACar.Application.Interfaces;
using RentACar.Domain;
using RentACar.DTOs.Garage;
using RentACar.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACar.Application.Services
{
    public class GarageService : IGarageService
    {
        private readonly IGarageRepository _garageRepository;
        public GarageService(IGarageRepository garageRepository)
        {
            _garageRepository = garageRepository;
        }

        public async Task<GetGarageDTO?> AddGarageAsync(AddGarageDTO addGarage)
        {
            Garage garage = new Garage()
            {
                GarageName = addGarage.GarageName,
                Location = addGarage.Location,
                EstablishDate = addGarage.EstablishDate
            };
            Garage? response = await _garageRepository.AddGarageAsync(garage);
            if (response == null)
            {
                return null;
            }
            GetGarageDTO responseDTO = new GetGarageDTO()
            {
                Id = response.Id,
                GarageName=response.GarageName,
                Location = response.Location,
                EstablishDate=response.EstablishDate,
                BalanceSheet = response.BalanceSheet
            };
            return responseDTO;
        }

        public async Task<bool> DeleteGarageAsync(int id)
        {
            bool response = await _garageRepository.DeleteGarageAsync(id);
            if (!response)
            {
                return false;
            }
            return true;
        }

        public async Task<GetGarageDTO> GetGarageAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<GetGaragesDTO>> GetGaragesAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<GetGarageDTO> UpdateGarageAsync()
        {
            throw new NotImplementedException();
        }
    }
}

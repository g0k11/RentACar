using RentACar.Application.Interfaces;
using RentACar.DTOs.Auth;
using RentACar.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACar.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IRenterRepository _renterRepository;
        public AuthService(IRenterRepository renterRepository)
        {
            _renterRepository = renterRepository;
        }

        public async Task<AuthResultDto> LoginAsync(LoginDTO login)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> RegisterAsync(RegisterDTO addRenter)
        {
            throw new NotImplementedException();
        }
    }
}

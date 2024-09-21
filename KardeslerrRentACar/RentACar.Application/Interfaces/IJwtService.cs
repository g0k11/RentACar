using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace RentACar.Application.Interfaces
{
    public interface IJwtService
    {
        Task<string> RefreshJwtToken(string refreshToken);
        ClaimsPrincipal ValidateJwtToken(string token);
    }
}

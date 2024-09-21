using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using RentACar.Application.Interfaces;
using RentACar.DTOs.Auth;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

public class JwtService : IJwtService
{
    private readonly JwtSettings _jwtSettings;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public JwtService(IOptions<JwtSettings> jwtSettings, IHttpContextAccessor httpContextAccessor)
    {
        _jwtSettings = jwtSettings.Value;
        _httpContextAccessor = httpContextAccessor;
    }

    public Task<string> RefreshJwtToken(string refreshToken)
    {
        var newJwtToken = GenerateJwtToken();
        Console.WriteLine("New Token: " + newJwtToken);
        return Task.FromResult(newJwtToken);
    }

    private string GenerateJwtToken()
    {
        var httpContext = _httpContextAccessor.HttpContext;
        var userEmail = httpContext?.User.FindFirst(ClaimTypes.Email)?.Value;
        var userRole = httpContext?.User.FindFirst(ClaimTypes.Role)?.Value;
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(_jwtSettings.SecretKey);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Email, userEmail ?? string.Empty),
                new Claim(ClaimTypes.Role, userRole ?? string.Empty)
            }),
            Expires = DateTime.UtcNow.AddMinutes(_jwtSettings.TokenLifetimeMinutes),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            Issuer = _jwtSettings.Issuer,
            Audience = _jwtSettings.Audience
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
    public ClaimsPrincipal ValidateJwtToken(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        try
        {
            var validationParameters = GetTokenValidationParameters();
            var principal = tokenHandler.ValidateToken(token, validationParameters, out var validatedToken);

            if (validatedToken is JwtSecurityToken jwtToken &&
                jwtToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            {
                return principal; 
            }
            return null; 
        }
        catch (SecurityTokenExpiredException ex)
        {
            Console.WriteLine("Token süresi dolmuş: " + ex.Message);
            return null;
        }
        catch (SecurityTokenInvalidSignatureException ex)
        {
            Console.WriteLine("Geçersiz token imzası: " + ex.Message);
            return null;
        }
        catch (Exception ex)
        {
            Console.WriteLine("Diğer bir hata oluştu: " + ex.Message);
            return null;
        }
    }

    private TokenValidationParameters GetTokenValidationParameters()
    {
        return new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = _jwtSettings.Issuer,
            ValidAudience = _jwtSettings.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey))
        };
    }
}

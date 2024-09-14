using Azure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using RentACar.Application.Interfaces;
using RentACar.Domain;
using RentACar.DTOs.Auth;
using RentACar.DTOs.User;
using RentACar.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using BCrypt.Net;
using Microsoft.Extensions.Configuration; // Add this to use BCrypt for password hashing

namespace RentACar.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IRenterRepository _renterRepository;
        private readonly IConfiguration _configuration;

        public AuthService(IConfiguration configuration, IRenterRepository renterRepository)
        {
            _configuration = configuration;
            _renterRepository = renterRepository;
        }
        public async Task<UserProfileDTO> GetUserProfileAsync(string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var jwtSettings = _configuration.GetSection("Jwt");
                var key = Encoding.ASCII.GetBytes(jwtSettings["SecretKey"]);

                var validationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings["Issuer"],
                    ValidAudience = jwtSettings["Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(key)
                };

                // Validate the token
                var principal = tokenHandler.ValidateToken(token, validationParameters, out var validatedToken);
                var email = principal.Claims.First(claim => claim.Type == ClaimTypes.Name).Value;

                var user = await _renterRepository.GetRenterByMailAsync(email);
                if (user == null)
                {
                    return null;
                }

                return new UserProfileDTO
                {
                    Name = user.User.Name,
                    Email = user.User.Email,
                    PhoneNumber = user.PhoneNumber,
                    Address = user.Address,
                    LicenseType = user.LicenseType,
                    RentCount = user.RentCount,
                    DateOfBirth = user.DateOfBirth,
                    Gender = user.Gender
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred: {ex.Message}");
                throw;
            }
        }



        public async Task<AuthResultDto> LoginAsync(LoginDTO login)
        {
            var user = await _renterRepository.GetRenterByMailAsync(login.Email);

            if (user != null && VerifyPassword(login.Password, user.User.PasswordHashed))
            {
                var token = GenerateToken(login.Email, "renter");
                var refreshToken = GenerateRefreshToken();

                var result = new AuthResultDto
                {
                    Token = token,
                    RefreshToken = refreshToken,
                    Mail = login.Email,
                    Expiration = DateTime.Now.AddMinutes(30),
                    IsSuccess = true
                };

                return result;
            }
            else
            {
                return new AuthResultDto
                {
                    IsSuccess = false,
                    ErrorMessage = "Kullanıcı adı veya şifre hatalı!"
                };
            }
        }

        public async Task<bool> RegisterAsync(RegisterDTO addRenter)
        {
            var user = new User
            {
                Name = addRenter.Name,
                Email = addRenter.Email,
                PasswordHashed = HashPassword(addRenter.Password),
                Role = "renter" // For now
            };

            var renter = new Renter
            {
                GovIdNumber = addRenter.GovIdNumber,
                LicenseType = addRenter.LicenseType,
                DateOfBirth = addRenter.DateOfBirth,
                PhoneNumber = addRenter.PhoneNumber,
                Gender = addRenter.Gender,
                Address = addRenter.Address,
                User = user
            };

            await _renterRepository.AddRenterAsync(renter);

            return true;
        }

        private string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password); 
        }

        private bool VerifyPassword(string password, string hashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }

        private string GenerateToken(string email, string role)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var jwtSettings = _configuration.GetSection("Jwt");
                var key = Encoding.ASCII.GetBytes(jwtSettings["SecretKey"]);

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new[]
                    {
                new Claim(ClaimTypes.Name, email),
                new Claim(ClaimTypes.Role, role)
            }),
                    Expires = DateTime.UtcNow.AddMinutes(int.Parse(jwtSettings["TokenLifetimeMinutes"])),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                    Issuer = jwtSettings["Issuer"],
                    Audience = jwtSettings["Audience"]
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);
                var tokenString = tokenHandler.WriteToken(token);

                Console.WriteLine($"Generated Token: {tokenString}");

                return tokenString;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Token generation failed: {ex.Message}");
                throw;
            }
        }


        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }
    }
}


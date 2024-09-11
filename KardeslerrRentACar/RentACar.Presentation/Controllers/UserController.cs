using Microsoft.AspNetCore.Mvc;
using RentACar.Application.Interfaces;
using RentACar.DTOs.Auth;
using RentACar.DTOs.User;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text.Json;

namespace RentACar.Presentation.Controllers
{
    public class UserController : Controller
    {
        private readonly IAuthService _authService;
        private readonly HttpClient _httpClient;
        public UserController(IAuthService authService, HttpClient httpClient)
        {
            _authService = authService;
            _httpClient = httpClient;
        }
        public async Task<IActionResult> Index()
        {
            var token = Request.Cookies["AuthToken"];
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Login", "Account");
            }

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _httpClient.GetAsync("/api/profile/GetProfile");

            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var userProfile = JsonSerializer.Deserialize<UserProfileDTO>(jsonResponse, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
                return View(userProfile);
            }

            return RedirectToAction("Login", "Account");
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginDTO loginDto)
        {
            var result = await _authService.LoginAsync(loginDto);

            if (result.IsSuccess)
            {
                
                Response.Cookies.Append("AuthToken", result.Token, new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.Strict,
                    Expires = result.Expiration
                });

                Response.Cookies.Append("RefreshToken", result.RefreshToken, new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.Strict,
                    Expires = DateTime.Now.AddDays(30)
                });

                ViewBag.Mail = result.Mail;
                return View("Index");
            }

            ViewBag.Error = result.ErrorMessage;
            return View();
        }
        [HttpGet("GetProfile")]
        public async Task<IActionResult> GetProfile()
        {
            var token = Request.Cookies["AuthToken"];
            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized();
            }

            var userProfile = await _authService.GetUserProfileAsync(token);
            if (userProfile == null)
            {
                return NotFound();
            }

            return Ok(userProfile);
        }
    }
}

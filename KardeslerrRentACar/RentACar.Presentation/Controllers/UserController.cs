using Microsoft.AspNetCore.Mvc;
using RentACar.Application.Interfaces;
using RentACar.DTOs.Auth;
using RentACar.DTOs.User;
using System.Net.Http.Headers;
using System.Text.Json;

namespace RentACar.Presentation.Controllers
{
    [Route("[Controller]")]
    public class UserController : Controller
    {
        private readonly IAuthService _authService;
        private readonly HttpClient _httpClient;

        public UserController(IAuthService authService, HttpClient httpClient)
        {
            _authService = authService;
            _httpClient = httpClient;
        }

        // Giriş yapma işlemi
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginDTO loginDto)
        {
            if (!ModelState.IsValid)
            {
                return View(loginDto);
            }
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

                return RedirectToAction("Index","Home");
            }

            ViewBag.Error = result.ErrorMessage;
            return View(result.ErrorMessage);
        }

        // Çıkış yapma işlemi
        [HttpPost("logout")]
        public IActionResult Logout()
        {
            Response.Cookies.Delete("AuthToken");
            Response.Cookies.Delete("RefreshToken");

            return RedirectToAction("Login");
        }

        // Profil bilgilerini getirme
        [HttpGet("Profile")]
        public async Task<IActionResult> Profile()
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

            return View(userProfile);
        }
    }
}

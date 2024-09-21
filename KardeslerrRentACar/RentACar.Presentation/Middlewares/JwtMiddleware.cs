using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using RentACar.Application.Interfaces;
using RentACar.DTOs.Auth;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;

public class JwtMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly JwtSettings _jwtSettings;

    public JwtMiddleware(RequestDelegate next,IServiceScopeFactory serviceScopeFactory, IOptions<JwtSettings> jwtSettings)
    {
        _next = next;
        _serviceScopeFactory = serviceScopeFactory;
        _jwtSettings = jwtSettings.Value;
    }

    public async Task Invoke(HttpContext context)
    {
        var endpoint = context.GetEndpoint();
        var isAuthorizationRequired = endpoint?.Metadata?.GetMetadata<AuthorizeAttribute>() != null;

        if (!isAuthorizationRequired)
        {
            await _next(context);
            return;
        }

        var token = context.Request.Cookies["AuthToken"];
        using (var scope = _serviceScopeFactory.CreateScope())
        {
            var _jwtService = scope.ServiceProvider.GetRequiredService<IJwtService>();
            if (token == null)
            {
                var refreshToken = context.Request.Cookies["RefreshToken"];

                if (refreshToken != null)
                {
                    var newToken = await _jwtService.RefreshJwtToken(refreshToken);
                    if (newToken != null)
                    {
                        context.Response.Cookies.Append("AuthToken", newToken, new CookieOptions
                        {
                            HttpOnly = true,
                            Secure = true,
                            SameSite = SameSiteMode.Strict,
                            Expires = DateTime.UtcNow.AddMinutes(_jwtSettings.TokenLifetimeMinutes)
                        });
                        context.Request.Headers["Authorization"] = "Bearer " + newToken;
                        await _next(context);
                        return;
                    }
                }

                context.Response.Redirect("/User");
                return;


            }
            context.Request.Headers["Authorization"] = "Bearer " + token;
        }
            await _next(context);
    }

}

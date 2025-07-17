using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TestLetshare.Application.Common.Models;
using TestLetshare.Application.Features.Auth.Commands;
using TestLetshare.Application.Features.Auth.Interfaces;
using TestLetshare.Domain.Entities;
using TestLetshare.Infrastructure.Data;

namespace TestLetshare.Infrastructure.Services
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext _db;
        private readonly IConfiguration _configuration;

        public AuthService(AppDbContext db, IConfiguration configuration)
        {
            _configuration = configuration;
            _db = db;
        }

        public async Task<ApiResponse<TokenResponse>> SignInAsync(SignInCommand command)
        {
            var user = await _db.Users.FirstOrDefaultAsync(u => u.Email == command.username);
            if (user == null || !BCrypt.Net.BCrypt.Verify(command.password, user.Password))
            {
                return new ApiResponse<TokenResponse>
                {
                    Success = false,
                    Message = "Invalid credentials"
                };
            }

            var accessToken =  GenerateJwtToken(user, _configuration, isRefreshToken: false );
            var refreshToken =  GenerateJwtToken(user, _configuration, isRefreshToken: true);

            TokenResponse tokenResponse = new TokenResponse()
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };

            return new ApiResponse<TokenResponse>
            {
                Success = true,
                Message = "Login successfully!",
                Data = tokenResponse,
            };
        }

        public string GenerateJwtToken(User user, IConfiguration config, bool isRefreshToken)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Secret"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var expiry = DateTime.UtcNow.AddMinutes(
                isRefreshToken
                    ? int.Parse(config["Jwt:RefreshTokenExpirationMinutes"]!)
                    : int.Parse(config["Jwt:AccessTokenExpirationMinutes"]!)
            );

            var claims = new List<Claim>
        {
            new Claim("userId", user.Id.ToString()),
            new Claim("email", user.Email),
            new Claim("role", user.Role ?? ""),
            new Claim("tenantId", user.TenantId ?? ""),
            new Claim("languageId", user.LanguageId ?? ""),
        };

            var token = new JwtSecurityToken(
                issuer: config["Jwt:Issuer"],
                audience: config["Jwt:Audience"],
                claims: claims,
                expires: expiry,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}

using TestLetshare.Application.Common.Models;
using TestLetshare.Application.Features.Auth.Commands;
using Microsoft.Extensions.Configuration;

namespace TestLetshare.Application.Features.Auth.Interfaces
{
    public interface IAuthService
    {
        Task<ApiResponse<TokenResponse>> SignInAsync(SignInCommand command);
        string GenerateJwtToken(Domain.Entities.User user, IConfiguration config, bool isRefreshToken);
    }
}

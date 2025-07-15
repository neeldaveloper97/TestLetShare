using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TestLetshare.Application.Common.Interfaces;
using TestLetshare.Application.Features.Auth.Interfaces;
using TestLetshare.Application.Features.User.Interface;
using TestLetshare.Infrastructure.Data;
using TestLetshare.Infrastructure.Repositories.Common;
using TestLetshare.Infrastructure.Services;
namespace TestLetshare.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));


            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IUserService, UserService>();

            return services;
        }
    }
}

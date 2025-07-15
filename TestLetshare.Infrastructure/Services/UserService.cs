using Microsoft.EntityFrameworkCore;
using TestLetshare.Application.Features.User.Interface;
using TestLetshare.Domain.Entities;
using TestLetshare.Infrastructure.Data;

namespace TestLetshare.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _db;

        public UserService(AppDbContext db)
        {
            _db = db;
        }
        public async Task<List<User>> GetAllUsers()
        {
            return await _db.Users.ToListAsync();
        }
    }
}

namespace TestLetshare.Application.Features.User.Interface
{
    public interface IUserService
    {
        Task<List<TestLetshare.Domain.Entities.User>> GetAllUsers();
    }
}

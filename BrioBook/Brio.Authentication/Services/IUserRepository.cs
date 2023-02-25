using Brio.Database.DAL.Models;

namespace Brio.Authentication.Services;

public interface IUserRepository : IRepository<User, int>
{
}

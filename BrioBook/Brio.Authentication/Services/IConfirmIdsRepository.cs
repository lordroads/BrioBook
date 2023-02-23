using BrioBook.Users.DAL.Models;

namespace Brio.Authentication.Services;

public interface IConfirmIdsRepository : IRepository<ConfirmId, Guid>
{
}

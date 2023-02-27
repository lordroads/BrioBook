using Brio.Database.DAL.Models;
using BrioBook.Client.Models.Response;

namespace BrioBook.Client.Services;

public interface IUsersClient
{
    public UserGetAllResponse GetAll();
    public SetAdminRoleResponse SetAdminRole(int userId, bool state);
    public DeleteUserResponse DeleteUser(int userId);
}

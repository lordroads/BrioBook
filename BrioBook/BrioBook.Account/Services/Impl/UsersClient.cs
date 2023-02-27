using Brio.Database.DAL.Models;
using BrioBook.Client.Models.Response;

namespace BrioBook.Client.Services.Impl;

public class UsersClient : IUsersClient
{
    private readonly HttpClient _httpClient;
    private Uri confirmAddress;
    //TODO: private readonly ILogger<MetricsAgentClient> _logger;

    public UsersClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
        confirmAddress = new Uri("http://localhost:0000");
    }

    public DeleteUserResponse DeleteUser(int userId)
    {
        throw new NotImplementedException();
    }

    public IList<User> GetAll()
    {
        throw new NotImplementedException();
    }

    public SetAdminRoleResponse SetAdminRole(int userId, bool state)
    {
        throw new NotImplementedException();
    }
}

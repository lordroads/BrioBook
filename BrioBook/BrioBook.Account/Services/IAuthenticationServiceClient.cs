using BrioBook.Client.Models.Response;

namespace BrioBook.Client.Services;

public interface IAuthenticationServiceClient
{
    public LoginResponse Login(string login, string password);
    public RegistrationResponse Registration(string login, string password);
}

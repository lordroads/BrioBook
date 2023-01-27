using BrioBook.Account.Models.Requests;
using EmployeeService.Models.Dto;

namespace BrioBook.Account.Services;

public interface IAuthenticateService
{
    AuthenticationResponse Login(AuthenticationRequest authenticationRequest);

    SessionDto GetSession(string sessionToken);
}

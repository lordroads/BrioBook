using Brio.Authentication.Models;
using Brio.Authentication.Models.Dto;
using Brio.Database.DAL.Models;
using System.Security.Claims;

namespace Brio.Authentication.Services;

public interface IAuthenticationService
{
    bool UpdateRole();
    AuthenticationUserData GetClaims(User user);
    (User user, bool Succeeded, string Errors) Login(UserDto userDto);
    (User user, bool Succeeded, string Errors) Create(UserDto userDto);
}

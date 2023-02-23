using Brio.Authentication.Models.Dto;
using BrioBook.Users.DAL.Models;
using System.Security.Claims;

namespace Brio.Authentication.Services;

public interface IAuthenticationService
{
    bool UpdateRole();
    ClaimsPrincipal GetClaims(User user);
    (User user, bool Succeeded, string Errors) Login(UserDto userDto);
    (User user, bool Succeeded, string Errors) Create(UserDto userDto);
}

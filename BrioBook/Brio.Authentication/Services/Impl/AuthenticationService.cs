using Brio.Authentication.Models.Dto;
using Brio.Authentication.Utils;
using BrioBook.Users.DAL.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;

namespace Brio.Authentication.Services.Impl;

public class AuthenticationService : IAuthenticationService
{
    private readonly IUserRepository _userRepository;

    public AuthenticationService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }


    public ClaimsPrincipal GetClaims(User user)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Email, user.Login),
            new Claim(ClaimTypes.Role, user.IsAdmin ? "Admin" : "User")
        };

        ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

        ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

        return claimsPrincipal;
    }

    public (User user, bool Succeeded, string Errors) Login(UserDto userDto)
    {
        User user = _userRepository.GetByValue(userDto.Login);

        if (user == null)
        {
            return (
                User: null,
                Succeeded: false, 
                Errors: "The data is entered incorrectly!");
        }

        if (!user.ConfirmEmail)
        {
            return (
                User: null,
                Succeeded: false,
                Errors: "User not confirm email!");
        }

        var result = PasswordUtil
            .VerifyPassword(userDto.Password, user.PasswordSalt, user.PasswordHash);

        if (!result)
        {
            return (
                User: null,
                Succeeded: false, 
                Errors: "The data is entered incorrectly!");
        }

        return (User: user, Succeeded: true, Errors: null);
    }

    public (User user, bool Succeeded, string Errors) Create(UserDto userDto)
    {
        User user = _userRepository.GetByValue(userDto.Login);

        if (user != null)
        {
            return (User: null, Succeeded: false, Errors: "There is already a user with that name!");
        }

        var result = PasswordUtil.CreatePasswordHash(userDto.Password);

        user = new User
        {
            Login = userDto.Login,
            PasswordHash = result.passwordHash,
            PasswordSalt = result.passwordSalt,
            IsAdmin = false
        };

        _userRepository.Create(user);

        return (User: user, Succeeded: true, Errors: null);
    }

    public bool UpdateRole()
    {
        throw new NotImplementedException();
    }
}

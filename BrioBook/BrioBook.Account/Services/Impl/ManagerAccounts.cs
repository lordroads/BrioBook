using BrioBook.Account.Models;
using BrioBook.Account.Utils;

namespace BrioBook.Account.Services.Impl;

public class ManagerAccounts : IManagerAccounts
{
    private readonly IUserRepository _userRepository;

    public ManagerAccounts(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<(bool Succeeded, string? Errors)> SingIn(string login, string password)
    {
        User user = _userRepository.GetByValue(login);

        if (user == null) 
        {
            return (Succeeded: false, Errors: "The data is entered incorrectly!");
        }

        var result = PasswordUtil.VerifyPassword(password, user.PasswordSalt, user.PasswordHash);

        if (!result)
        {
            return (Succeeded: false, Errors: "The data is entered incorrectly!");
        }

        return (Succeeded: true, Errors: null);
    }

    public async Task<(bool Succeeded, string? Errors)> SingUp(string login, string password)
    {
        User user = _userRepository.GetByValue(login);

        if (user != null)
        {
            return (Succeeded: false, Errors: "There is already a user with that name!");
        }

        var result = PasswordUtil.CreatePasswordHash(password);

        _userRepository.Add(new User
        {
            Login = login,
            PasswordHash = result.passwordHash,
            PasswordSalt = result.passwordSalt
        });

        return (Succeeded: true, Errors: null);
    }
}

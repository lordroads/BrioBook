namespace BrioBook.Account.Services;

public interface IManagerAccounts
{
    Task<(bool Succeeded, string Errors)> SingIn(string login, string password);
    Task<(bool Succeeded, string Errors)> SingUp(string login, string password);
}
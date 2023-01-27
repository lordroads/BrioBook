using System.Security.Cryptography;
using System.Text;

namespace BrioBook.Account.Utils;

public class PasswordUtil
{
    public static (string passwordSalt, string passwordHash) CreatePasswordHash(string password)
    {
        //Generate random salt
        byte[] buffer = new byte[16];

        RandomNumberGenerator.Fill(buffer);

        string passwordSalt = Convert.ToBase64String(buffer);

        //Call method for give password hash
        string passwordHash = GetPasswordHash(password, passwordSalt);

        return (passwordSalt, passwordHash);
    }

    public static bool VerifyPassword(string password, string passwordSalt, string passworHash)
    {
        return GetPasswordHash(password, passwordSalt) == passworHash;
    }

    public static string GetPasswordHash(string password, string passwordSalt)
    {
        //create string password
        password = $"{password}~{passwordSalt}~{Environment.GetEnvironmentVariable("SECRET_KEY")}";
        byte[] buffer = Encoding.UTF8.GetBytes(password);

        //get hash
        SHA512 sha512 = new SHA512Managed();
        byte[] passwordHash = sha512.ComputeHash(buffer);

        return Convert.ToBase64String(passwordHash);
    }
}
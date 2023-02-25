using System.Security.Claims;

namespace BrioBook.Client.Models;

public class AuthenticationUserData
{
    public string AuthenticationScheme { get; set; }
    public Dictionary<string,string> Claims { get; set; }
}

using Swashbuckle.AspNetCore.SwaggerGen;
using System.Security.Claims;

namespace Brio.Authentication.Models;

public class AuthenticationUserData
{
    public string AuthenticationScheme { get; set; }
    public Dictionary<string, string> Claims { get; set; } = new Dictionary<string, string>();
}

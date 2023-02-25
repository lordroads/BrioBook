using System.Security.Claims;

namespace Brio.Authentication.Models.Response;

public class AuthenticationDataResponse : BaseResponse
{
    public AuthenticationUserData AuthenticationUserData { get; set; }
}

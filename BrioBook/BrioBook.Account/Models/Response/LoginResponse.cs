namespace BrioBook.Client.Models.Response;

public class LoginResponse : BaseResponse
{
    public AuthenticationUserData AuthenticationUserData { get; set; }
}

using BrioBook.Client.Models.Dto;

namespace BrioBook.Client.Models.Response;

public class AuthenticationResponse
{
    public AuthenticationStatus Status { get; set; }
    public SessionDto Session { get; set; }
}
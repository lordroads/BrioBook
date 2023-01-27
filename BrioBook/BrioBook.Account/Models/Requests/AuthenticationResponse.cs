using EmployeeService.Models.Dto;

namespace BrioBook.Account.Models.Requests;

public class AuthenticationResponse
{
    public AuthenticationStatus Status { get; set; }
    public SessionDto Session { get; set; }
}
using Brio.Authentication.Models;
using Brio.Authentication.Models.Dto;
using Brio.Authentication.Models.Request;
using Brio.Authentication.Models.Response;
using Brio.Authentication.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Brio.Authentication.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthenticationController : Controller
{
    private readonly IConfirmServiceClient _confirmServiceClient;
    private readonly IOptions<Settings> _options;
    private readonly IAuthenticationService _authenticationService;

    public AuthenticationController(IAuthenticationService authenticationService, IOptions<Settings> options, IConfirmServiceClient confirmServiceClient)
    {
        _authenticationService = authenticationService;
        _options = options;
        _confirmServiceClient = confirmServiceClient;
    }


    [HttpPost("login")]
    public ActionResult<AuthenticationDataResponse> Login([FromForm] AuthenticationDataRequest request)
    {
        var response = _authenticationService.Login(new UserDto
        {
            Login = request.Login,
            Password = request.Password
        });

        if (!response.Succeeded)
        {
            return BadRequest(new AuthenticationDataResponse
            {
                Succeeded = response.Succeeded,
                Errors = response.Errors
            });
        }

        return Ok(new AuthenticationDataResponse
        {
            Succeeded = response.Succeeded,
            Errors = response.Errors,
            AuthenticationUserData = _authenticationService.GetClaims(response.user)
        });
    }

    [HttpPost("registration")]
    public ActionResult<RegistrationResponse> Create([FromForm] RegistrationRequest request)
    {
        var response = _authenticationService.Create(new UserDto
        {
            Login = request.Email,
            Password = request.Password
        });

        if (!response.Succeeded)
        {
            return BadRequest(new RegistrationResponse
            {
                Succeeded = response.Succeeded,
                Errors = response.Errors
            });
        }

        var confirmId = _confirmServiceClient.CreateConfirmToUser(response.user.Id);

        if (confirmId is null)
        {
            return BadRequest(new RegistrationResponse
            {
                Succeeded = response.Succeeded,
                Errors = "Not create confirm ID."
            });
        }

        return Ok(new RegistrationResponse
        {
            ConfirmId = confirmId,
            Errors = response.Errors,
            Succeeded = response.Succeeded
        });
    }
}

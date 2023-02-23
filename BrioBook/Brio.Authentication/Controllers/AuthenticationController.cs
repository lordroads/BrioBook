using Brio.Authentication.Models.Dto;
using Brio.Authentication.Models.Request;
using Brio.Authentication.Models.Response;
using Brio.Authentication.Services;
using BrioBook.Users.DAL.Models;
using Microsoft.AspNetCore.Mvc;

namespace Brio.Authentication.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticationController : Controller
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IConfirmIdsRepository _conrimIdsRepository;

        public AuthenticationController(IAuthenticationService authenticationService, IConfirmIdsRepository conrimIdsRepository)
        {
            _authenticationService = authenticationService;
            _conrimIdsRepository = conrimIdsRepository; //TODO: Отдельный сервис
        }


        [HttpPost("login")]
        public ActionResult<AuthenticationDataResponse> Login([FromBody] AuthenticationDataRequest request)
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
                ClaimsPrincipal = _authenticationService.GetClaims(response.user)
            });
        }

        [HttpPost("registration")]
        public ActionResult<RegistrationResponse> Create([FromBody] RegistrationRequest request)
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

            var confirmId = _conrimIdsRepository.Create(new ConfirmId 
            { 
                UserId = response.user.UserId
            });

            return Ok(new RegistrationResponse
            {
                Succeeded = response.Succeeded,
                Errors = response.Errors,
                ConfirmId = confirmId
            });
        }
    }
}

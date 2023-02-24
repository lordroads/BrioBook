using Brio.Authentication.Models;
using Brio.Authentication.Models.Dto;
using Brio.Authentication.Models.Request;
using Brio.Authentication.Models.Response;
using Brio.Authentication.Services;
using BrioBook.Users.DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Net.Http;
using System.Text.Json;
using System.Text;

namespace Brio.Authentication.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticationController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly IOptions<Settings> _options;
        private readonly IAuthenticationService _authenticationService;

        public AuthenticationController(IAuthenticationService authenticationService, HttpClient httpClient, IOptions<Settings> options)
        {
            _authenticationService = authenticationService;
            _httpClient = httpClient;
            _options = options;
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

            Uri confirmAddress = new Uri("http://localhost:5187");

            var httpRequest = new HttpRequestMessage(HttpMethod.Post, $"{confirmAddress}api/create");
            httpRequest.Content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("UserId", response.user.UserId.ToString())
            });

            try
            {
                HttpResponseMessage httpResponse = _httpClient.SendAsync(httpRequest).Result;

                if (httpResponse.IsSuccessStatusCode)
                {
                    var answer = httpResponse.Content.ReadAsStringAsync().Result;

                    //return (AllHddMetricsApiResponse)JsonConvert.DeserializeObject(answer, typeof(AllHddMetricsApiResponse));
                    return Ok(new RegistrationResponse
                    {
                        Succeeded = response.Succeeded,
                        Errors = response.Errors,
                        ConfirmId = Guid.Parse(answer)
                    });
                }

                return BadRequest(new RegistrationResponse
                {
                    Succeeded = httpResponse.IsSuccessStatusCode,
                    Errors = httpResponse.StatusCode + "\n" + httpResponse.RequestMessage
                });

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest(new RegistrationResponse
                {
                    Succeeded = false,
                    Errors = response.Errors + "\n Owner message: " + ex.Message
                });
            }
        }
    }
}

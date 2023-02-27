using BrioBook.Client.Models.Response;
using Newtonsoft.Json;

namespace BrioBook.Client.Services.Impl;

public class AuthenticationServiceClient : IAuthenticationServiceClient
{
    private readonly HttpClient _httpClient;
    private Uri authenticationAddress;
    //TODO: private readonly ILogger<MetricsAgentClient> _logger;

    public AuthenticationServiceClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
        authenticationAddress = new Uri(Environment.GetEnvironmentVariable("AUTHENTICATION_ADDRESS"));
    }
    public LoginResponse Login(string login, string password)
    {
        var httpRequest = new HttpRequestMessage(HttpMethod.Post, $"{authenticationAddress}api/Authentication/login");

        httpRequest.Content = new FormUrlEncodedContent(
                new Dictionary<string, string>
                {
                    { "login", login },
                    { "password", password}
                });

        try
        {
            HttpResponseMessage httpResponse = _httpClient.SendAsync(httpRequest).Result;

            var answer = httpResponse.Content.ReadAsStringAsync().Result;

            LoginResponse? response =
                (LoginResponse)JsonConvert
                .DeserializeObject(answer, typeof(LoginResponse));

            return response;

        }
        catch (Exception ex)
        {
            return new LoginResponse
            {
                Errors = ex.Message,
                Succeeded = false
            };
        }
    }

    public RegistrationResponse Registration(string login, string password)
    {
        var httpRequest = new HttpRequestMessage(HttpMethod.Post, $"{authenticationAddress}api/Authentication/registration");

        httpRequest.Content = new FormUrlEncodedContent(
                new Dictionary<string, string>
                {
                    { "Email", login },
                    { "Password", password}
                });

        try
        {
            HttpResponseMessage httpResponse = _httpClient.SendAsync(httpRequest).Result;

            if (httpResponse.IsSuccessStatusCode)
            {
                var answer = httpResponse.Content.ReadAsStringAsync().Result;

                RegistrationResponse? response = (RegistrationResponse)JsonConvert.DeserializeObject(answer, typeof(RegistrationResponse));

                if (!response.Succeeded)
                {
                    return null;
                }

                return response;
            }

            return null;

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return null;
        }
    }
}

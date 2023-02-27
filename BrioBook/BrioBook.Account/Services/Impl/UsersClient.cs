using Brio.Database.DAL.Models;
using BrioBook.Client.Models.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.ResponseCompression;
using Newtonsoft.Json;

namespace BrioBook.Client.Services.Impl;

public class UsersClient : IUsersClient
{
    private readonly HttpClient _httpClient;
    private Uri confirmAddress;
    //TODO: private readonly ILogger<MetricsAgentClient> _logger;

    public UsersClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
        confirmAddress = new Uri(Environment.GetEnvironmentVariable("USER_MANAGEMENT_ADDRESS"));
    }

    public DeleteUserResponse DeleteUser(int userId)
    {
        throw new NotImplementedException();
    }

    public UserGetAllResponse GetAll()
    {
        HttpRequestMessage httpRequest = new HttpRequestMessage(HttpMethod.Get, $"{confirmAddress}api/Users/get-all/");

        try
        {
            var httpResponse = _httpClient.SendAsync(httpRequest).Result;

            var answer = httpResponse.Content.ReadAsStringAsync().Result;

            UserGetAllResponse response = 
                (UserGetAllResponse)JsonConvert
                .DeserializeObject(answer, typeof(UserGetAllResponse));

            return response;

        }
        catch (Exception ex)
        {
            return new UserGetAllResponse
            {
                Succeeded = false,
                Errors = ex.Message
            };
        }
    }

    public SetAdminRoleResponse SetAdminRole(int userId, bool state)
    {
        throw new NotImplementedException();
    }
}

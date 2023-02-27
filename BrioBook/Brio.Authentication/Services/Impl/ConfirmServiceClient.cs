using Brio.Authentication.Models.Response;
using System;
using Newtonsoft.Json;

namespace Brio.Authentication.Services.Impl;

public class ConfirmServiceClient : IConfirmServiceClient
{
    private readonly HttpClient _httpClient;
    private Uri confirmAddress;
    //TODO: private readonly ILogger<MetricsAgentClient> _logger;

    public ConfirmServiceClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
        confirmAddress = new Uri(Environment.GetEnvironmentVariable("CONFIRMATION_SERVICE_ADDRESS"));
    }
    public string? CreateConfirmToUser(int userId)
    {
        var httpRequest = new HttpRequestMessage(HttpMethod.Post, $"{confirmAddress}api/Confirm/create");

        httpRequest.Content = new FormUrlEncodedContent(new[]
        {
                new KeyValuePair<string, string>("userId", userId.ToString())
        });

        var values = new Dictionary<string, string>
          {
              { "userId", userId.ToString() }
          };

        var content = new FormUrlEncodedContent(values);

        try
        {
            HttpResponseMessage httpResponse = _httpClient.PostAsync($"{confirmAddress}api/Confirm/create", content).Result;

            if (httpResponse.IsSuccessStatusCode)
            {
                var answer = httpResponse.Content.ReadAsStringAsync().Result;

                RegistrationResponse? response = (RegistrationResponse)JsonConvert.DeserializeObject(answer, typeof(RegistrationResponse));

                if (!response.Succeeded)
                {
                    return null;
                }

                return response.ConfirmId;
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
using BrioBook.Client.Models.Response;
using Newtonsoft.Json;

namespace BrioBook.Client.Services.Impl;

public class ConfirmClient : IConfirmClient
{
    private readonly HttpClient _httpClient;
    private Uri confirmAddress;
    //TODO: private readonly ILogger<MetricsAgentClient> _logger;

    public ConfirmClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
        confirmAddress = new Uri(Environment.GetEnvironmentVariable("CONFIRMATION_ADDRESS"));
    }

    public SetConfirmResponse SetConfirm(Guid id)
    {
        HttpRequestMessage httpRequest = new HttpRequestMessage(HttpMethod.Post, $"{confirmAddress}api/Confirm/set-confirm/{id}");

        try
        {
            HttpResponseMessage httpResponse = _httpClient.SendAsync(httpRequest).Result;

            var answer = httpResponse.Content.ReadAsStringAsync().Result;

            SetConfirmResponse? response =
                (SetConfirmResponse)JsonConvert
                .DeserializeObject(answer, typeof(SetConfirmResponse));

            return response;

        }
        catch (Exception ex)
        {
            return new SetConfirmResponse
            {
                Errors = ex.Message,
                Succeeded = false
            };
        }
    }
}

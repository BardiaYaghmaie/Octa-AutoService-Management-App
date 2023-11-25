using System.Net.Http;
using System.Text.Json;

namespace OAS.Blazor.Services;

public class HttpRequestSender : IHttpRequestSender    
{
    private readonly IConfiguration _configuration;
    private readonly string apiUrl;
    private readonly HttpClient _httpClient;
    public HttpRequestSender(IConfiguration configuration, HttpClient httpClient)
    {
        _configuration = configuration;
        apiUrl = configuration.GetSection("apiUrl").Value;
        _httpClient = httpClient;
    }

    public async Task<HttpResponseMessage> SendAsync<TRequest>(TRequest request, string url, HttpMethod httpMethod) where TRequest : class
    {
        string jsonData = JsonSerializer.Serialize(request);
        var httpRequestMessage = new HttpRequestMessage
        {
            Method = httpMethod,
            RequestUri = new Uri(apiUrl + url),
            Content = new StringContent(jsonData, System.Text.Encoding.UTF8, "application/json")
        };
        HttpResponseMessage httpResponse = await _httpClient.SendAsync(httpRequestMessage);
        return httpResponse;
    }
}

using System.Net.Http;
using System.Text.Json;
using System.Net.Http.Headers;
using System.Net;
using Microsoft.AspNetCore.Authentication;
using Newtonsoft.Json;
using System.Web;
namespace OAS.Blazor.Services;

public class HttpRequestSender : IHttpRequestSender
{
    private readonly IConfiguration _configuration;
    private readonly string apiUrl;
    private readonly HttpClient _httpClient;
    private readonly IHttpContextAccessor _httpContextAccessor;
    public HttpRequestSender(IConfiguration configuration, HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
    {
        _configuration = configuration;
        apiUrl = configuration.GetSection("apiUrl").Value;
        _httpClient = httpClient;
        _httpContextAccessor = httpContextAccessor;
    }
    string ObjToQueryString(object obj)
    {
        var step1 = JsonConvert.SerializeObject(obj);

        var step2 = JsonConvert.DeserializeObject<IDictionary<string, string>>(step1);

        var step3 = step2.Select(x => HttpUtility.UrlEncode(x.Key) + "=" + HttpUtility.UrlEncode(x.Value));

        return string.Join("&", step3);
    }
    public async Task<HttpResponseMessage> SendAsync<TRequest>(TRequest request, string url, HttpMethod httpMethod) where TRequest : class
    {
        using var httpClient = new HttpClient();
        HttpRequestMessage httpRequestMessage;
        var accessToken = await _httpContextAccessor.HttpContext.GetTokenAsync("access_token");
        httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
        if (httpMethod == HttpMethod.Get || httpMethod == HttpMethod.Delete)
        {
            string queryString = this.ObjToQueryString(request);
            if (!string.IsNullOrEmpty(queryString)) queryString = "?" + queryString;
            httpRequestMessage = new HttpRequestMessage
            {

                Method = httpMethod,
                RequestUri = new Uri(apiUrl + url + queryString ),
            };
        }
        else
        {
            string jsonData = Newtonsoft.Json.JsonConvert.SerializeObject(request);
            httpRequestMessage = new HttpRequestMessage
            {

                Method = httpMethod,
                RequestUri = new Uri(apiUrl + url),
                Content = new StringContent(jsonData, System.Text.Encoding.UTF8, "application/json")
            };
        }
        HttpResponseMessage httpResponse = await httpClient.SendAsync(httpRequestMessage);
        return httpResponse;
    }
}

namespace OAS.Blazor.Services;

public interface IHttpRequestSender 
{
    Task<HttpResponseMessage> SendAsync<TRequest>(TRequest request,string url, HttpMethod httpMethod) where TRequest : class;
}

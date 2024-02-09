using System.Net.Http.Json;

namespace TestTask.WebApi.IntegrationTests;

public static class Extensions
{
    public static HttpClient WithBearerToken(this HttpClient httpClient, string token)
    {
        httpClient
            .DefaultRequestHeaders
            .Add("Authorization", $"Bearer {token}");

        return httpClient;
    }

    public static async Task<T?> DeserializeToAsync<T>(this HttpResponseMessage httpResponseMessage)
    {
        return await httpResponseMessage
            .Content
            .ReadFromJsonAsync<T>();
    }
}

using Microsoft.Extensions.Options;
using nApps.Futs.Mobile.Shared.Configuration;
using System;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Text;

namespace nApps.Futs.Mobile.Shared.Services.Api;

public class ApiService : IApiService
{
    private readonly HttpClient _httpClient;

    public ApiService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<T?> GetAsync<T>(string endpoint)
    {
        return await _httpClient.GetFromJsonAsync<T>(endpoint);
    }

    public async Task<TResponse?> PostAsync<TRequest, TResponse>(string endpoint,TRequest request)
    {
        var response = await _httpClient.PostAsJsonAsync(endpoint, request);

        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<TResponse>();
    }

    public async Task PostAsync(string endpoint)
    {
        var response = await _httpClient.PostAsync(endpoint, content: null);

        response.EnsureSuccessStatusCode();
    }

    public async Task<TResponse?> PostFormAsync<TResponse>(string endpoint,Dictionary<string, string> formData)
    {
        var content = new FormUrlEncodedContent(formData);

        var response = await _httpClient.PostAsync(endpoint, content);

        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<TResponse>();
    }

    public async Task<TResponse?> PutAsync<TRequest, TResponse>(string endpoint,TRequest request)
    {
        var response = await _httpClient.PutAsJsonAsync(endpoint, request);

        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<TResponse>();
    }

    public async Task DeleteAsync(string endpoint)
    {
        var response = await _httpClient.DeleteAsync(endpoint);

        response.EnsureSuccessStatusCode();
    }
}

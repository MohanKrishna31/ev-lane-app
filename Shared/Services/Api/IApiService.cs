using System;
using System.Collections.Generic;
using System.Text;

namespace nApps.Futs.Mobile.Shared.Services.Api;

public interface IApiService
{
    Task<T?> GetAsync<T>(string endpoint);
    Task<TResponse?> PostAsync<TRequest, TResponse>(string endpoint, TRequest request);
    Task PostAsync(string endpoint);
    Task<TResponse?> PostFormAsync<TResponse>(string endpoint, Dictionary<string, string> formData);

    Task<TResponse?> PutAsync<TRequest, TResponse>(string endpoint, TRequest request);
    Task DeleteAsync(string endpoint);
}

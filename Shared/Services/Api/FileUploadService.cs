using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;

namespace nApps.Futs.Mobile.Shared.Services.Api;

public class FileUploadService : IFileUploadService
{
    private readonly HttpClient _httpClient;

    public FileUploadService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<TResponse?> UploadAsync<TResponse>(string endpoint,Stream stream,string fileName, string contentType)
    {
        using var form = new MultipartFormDataContent();

        using var fileContent = new StreamContent(stream);

        fileContent.Headers.ContentType = new MediaTypeHeaderValue(contentType);

        form.Add( fileContent,"file", fileName);

        var response = await _httpClient.PostAsync(endpoint, form);

        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<TResponse>();
    }
}

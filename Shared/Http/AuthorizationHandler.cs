using nApps.Futs.Mobile.Shared.Constants;
using nApps.Futs.Mobile.Shared.Services.Storage;
using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text;

namespace nApps.Futs.Mobile.Shared.Http;

public class AuthorizationHandler : DelegatingHandler
{
    private readonly IStorageService _storageService;

    public AuthorizationHandler(IStorageService storageService)
    {
        _storageService = storageService;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,CancellationToken cancellationToken)
    {
        var token = await _storageService.GetSecureAsync(StorageKeys.AccessToken);

        if (!string.IsNullOrWhiteSpace(token))
        {
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        return await base.SendAsync(request, cancellationToken);
    }
}
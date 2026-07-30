using System;
using System.Collections.Generic;
using System.Text;

namespace nApps.Futs.Mobile.Shared.Services.Api;

public interface IFileUploadService
{
    Task<TResponse?> UploadAsync<TResponse>(string endpoint,Stream stream,string fileName,string contentType);
}

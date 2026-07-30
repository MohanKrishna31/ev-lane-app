using nApps.Futs.Mobile.Shared.Constants;
using nApps.Futs.Mobile.Shared.Services.Api;
using System;
using System.Collections.Generic;
using System.Text;

namespace nApps.Futs.Mobile.Features.Customer;

public class CustomerService : ICustomerService
{
    private readonly IApiService _apiService;
    private readonly IFileUploadService _fileUploadService;

    public CustomerService(IApiService apiService, IFileUploadService fileUploadService)
    {
        _apiService = apiService;
        _fileUploadService = fileUploadService;
    }

    public async Task<CustomerDto?> GetProfileAsync()
    {
        return await _apiService.GetAsync<CustomerDto>("api/app/customer/profile");
    }
    public async Task<CustomerDto?> UpdateProfileAsync(UpdateCustomerProfileRequest request)
    {
        return await _apiService.PutAsync<UpdateCustomerProfileRequest,CustomerDto>("api/app/customer/profile",request);
    }
    public async Task<ProfilePhotoDto?> UploadProfilePhotoAsync(Stream stream,string fileName,string contentType)
    {
        return await _fileUploadService.UploadAsync<ProfilePhotoDto>("api/app/customer/profile-photo",stream,fileName,contentType);
    }
    public async Task DeleteProfilePhotoAsync()
    {
        await _apiService.DeleteAsync("api/app/customer/profile-photo");
    }
}

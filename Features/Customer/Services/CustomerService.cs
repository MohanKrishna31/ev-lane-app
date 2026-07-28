using nApps.Futs.Customers;
using nApps.Futs.Mobile.Shared.Constants;
using nApps.Futs.Mobile.Shared.Services.Api;
using System;
using System.Collections.Generic;
using System.Text;

namespace nApps.Futs.Mobile.Features.Customer.Services;

public class CustomerService : ICustomerService
{
    private readonly IApiService _apiService;

    public CustomerService(IApiService apiService)
    {
        _apiService = apiService;
    }

    public async Task<CustomerDto?> GetProfileAsync()
    {
        return await _apiService.GetAsync<CustomerDto>(ApiRoutes.Customer.Profile);
    }
}

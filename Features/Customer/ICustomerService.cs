using System;
using System.Collections.Generic;
using System.Text;

namespace nApps.Futs.Mobile.Features.Customer;

public interface ICustomerService
{
    Task<CustomerDto?> GetProfileAsync();
    Task<CustomerDto?> UpdateProfileAsync(UpdateCustomerProfileRequest request);
}

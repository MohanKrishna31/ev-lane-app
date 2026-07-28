using nApps.Futs.Customers;
using System;
using System.Collections.Generic;
using System.Text;

namespace nApps.Futs.Mobile.Features.Customer.Services;

public interface ICustomerService
{
    Task<CustomerDto?> GetProfileAsync();
}

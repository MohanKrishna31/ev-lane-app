using nApps.Futs.Mobile.Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace nApps.Futs.Mobile.Features.Customer;

public class CustomerViewModel : BaseViewModel
{
    private readonly ICustomerService _customerService;

    public CustomerDto? Customer { get; private set; }

    public CustomerViewModel(ICustomerService customerService)
    {
        _customerService = customerService;
    }

    public async Task LoadAsync()
    {
        await ExecuteAsync(async () =>
        {
            Customer = await _customerService.GetProfileAsync();

            OnPropertyChanged(nameof(Customer));
        });
    }
}

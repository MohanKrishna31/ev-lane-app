using nApps.Futs.Mobile.Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace nApps.Futs.Mobile.Features.Customer;

public class EditProfileViewModel : BaseViewModel
{
    private readonly ICustomerService _customerService;

    public UpdateCustomerProfileRequest Model { get; } = new();

    public EditProfileViewModel(
        ICustomerService customerService)
    {
        _customerService = customerService;
    }

    public async Task LoadAsync()
    {
        await ExecuteAsync(async () =>
        {
            var customer = await _customerService.GetProfileAsync();

            if (customer is null)
                return;

            Model.FullName = customer.FullName;
            Model.Email = customer.Email;
            Model.DateOfBirth = customer.DateOfBirth;
            Model.Gender = customer.Gender;
            Model.PreferredLanguage = customer.PreferredLanguage;

            OnPropertyChanged(nameof(Model));
        });
    }

    public async Task SaveAsync()
    {
        await ExecuteAsync(async () =>
        {
            await _customerService.UpdateProfileAsync(Model);
        });
    }
}

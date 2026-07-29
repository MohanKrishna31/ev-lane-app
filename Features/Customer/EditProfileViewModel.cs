using nApps.Futs.Mobile.Shared.Constants;
using nApps.Futs.Mobile.Shared.Models;
using nApps.Futs.Mobile.Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace nApps.Futs.Mobile.Features.Customer;

public class EditProfileViewModel : BaseViewModel
{
    private readonly ICustomerService _customerService;
    public CustomerDto? Customer { get; private set; }
    public UpdateCustomerProfileRequest Model { get; } = new();

    public EditProfileViewModel(ICustomerService customerService)
    {
        _customerService = customerService;
    }

    public async Task LoadAsync()
    {
        await ExecuteAsync(async () =>
        {
            Customer = await _customerService.GetProfileAsync();

            if (Customer is null)
                return;

            Model.FullName = Customer.FullName;
            Model.Email = Customer.Email;
            Model.DateOfBirth = Customer.DateOfBirth;
            Model.Gender = Customer.Gender;
            Model.PreferredLanguage = Customer.PreferredLanguage;

            OnPropertyChanged(nameof(Model));
            OnPropertyChanged(nameof(Customer));
        });
    }

    public async Task SaveAsync()
    {
        await ExecuteAsync(async () =>
        {
            await _customerService.UpdateProfileAsync(Model);
        });
    }
    public IReadOnlyList<SelectOption<Gender>> GenderOptions { get; } =
    [
        new() { Value = Gender.Male, Text = "Male" },
        new() { Value = Gender.Female, Text = "Female" },
        new() { Value = Gender.Unknown, Text = "Unknown" },
        new() { Value = Gender.Other, Text = "Other" }
    ];
}

using nApps.Futs.Mobile.Shared.Constants;
using nApps.Futs.Mobile.Shared.Media;
using nApps.Futs.Mobile.Shared.Models;
using nApps.Futs.Mobile.Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace nApps.Futs.Mobile.Features.Customer;

public class EditProfileViewModel : BaseViewModel
{
    private readonly ICustomerService _customerService;
    public CustomerDto? Customer { get; private set; }
    public UpdateCustomerProfileRequest Model { get; } = new();

    private readonly IMediaPickerService _mediaPickerService;

    public EditProfileViewModel(ICustomerService customerService, IMediaPickerService mediaPickerService)
    {
        _customerService = customerService;
        _mediaPickerService = mediaPickerService;
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

    public async Task UploadPhotoAsync()
    {
        await ExecuteAsync(async () =>
        {
            var file = await _mediaPickerService.PickPhotoAsync();

            if (file == null)
                return;

            await using (file.Stream)
            {
                var result = await _customerService.UploadProfilePhotoAsync(
                    file.Stream,
                    file.FileName,
                    file.ContentType);

                // Instantly update the local UI with the new URL returned by the server
                if (result != null && Customer != null)
                {
                    Customer.ProfilePhotoUrl = result.ProfilePhotoUrl;
                    Customer.ProfilePhoto = result.ProfilePhoto;
                    OnPropertyChanged(nameof(Customer));
                }
            }
            // Intentionally NOT calling LoadAsync() here so we don't accidentally fetch stale data
        });
    }

    public async Task DeletePhotoAsync()
    {
        await ExecuteAsync(async () =>
        {
            // Instantly clear the local UI data first
            if (Customer != null)
            {
                Customer.ProfilePhotoUrl = null;
                Customer.ProfilePhoto = null;
                OnPropertyChanged(nameof(Customer));
            }

            await _customerService.DeleteProfilePhotoAsync();
            // Intentionally NOT calling LoadAsync() here so we don't accidentally fetch stale data
        });
    }
}
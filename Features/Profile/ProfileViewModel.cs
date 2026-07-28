using nApps.Futs.Mobile.Features.Authentication;
using nApps.Futs.Mobile.Shared.Services.Storage;
using nApps.Futs.Mobile.Shared.ViewModels;

namespace nApps.Futs.Mobile.Features.Profile.ViewModels;

public class ProfileViewModel : BaseViewModel
{
    private readonly IStorageService _storageService;
    private readonly IAuthenticationService _authenticationService;


    public ProfileViewModel(
        IStorageService storageService,
        IAuthenticationService authenticationService)
    {
        _storageService = storageService;
        _authenticationService = authenticationService;
    }

    public string UserName { get; private set; } = "EV User";

    public string MobileNumber { get; private set; } = "";



    public async Task LoadAsync()
    {
        // Later we will load from Customer API

        await Task.CompletedTask;
    }



    public async Task LogoutAsync()
    {
        await _authenticationService.LogoutAsync();
    }
}
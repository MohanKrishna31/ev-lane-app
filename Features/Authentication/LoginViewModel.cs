using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Extensions.Options;
using nApps.Futs.Mobile.Shared.Configuration;
using nApps.Futs.Mobile.Shared.ViewModels;
using System.Linq;
using System.Threading.Tasks;

namespace nApps.Futs.Mobile.Features.Authentication.ViewModels;

public partial class LoginViewModel : BaseViewModel
{
    private readonly IAuthenticationService _authenticationService;
    private readonly AuthenticationSettings _authenticationSettings;


    public LoginViewModel(
        IAuthenticationService authenticationService,
        IOptions<AuthenticationSettings> authenticationSettings)
    {
        _authenticationService = authenticationService;
        _authenticationSettings = authenticationSettings.Value;
    }


    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsMobileNumberValid))]
    public partial string MobileNumber { get; set; } = string.Empty;


    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsOtpValid))]
    public partial string Otp { get; set; } = string.Empty;


    [ObservableProperty]
    public partial bool OtpSent { get; set; }

    // Validation Properties
    public bool IsMobileNumberValid => !string.IsNullOrWhiteSpace(MobileNumber) && MobileNumber.All(char.IsDigit) && MobileNumber.Length >= 10;
    public bool IsOtpValid => !string.IsNullOrWhiteSpace(Otp) && Otp.Length == 6 && Otp.All(char.IsDigit);

    public async Task SendOtpAsync()
    {
        if (string.IsNullOrWhiteSpace(MobileNumber))
        {
            ErrorMessage = "Please enter mobile number.";
            return;
        }


        await ExecuteAsync(async () =>
        {
            await _authenticationService.SendOtpAsync(
                new SendOtpRequest
                {
                    MobileNumber = MobileNumber
                });


            OtpSent = true;
        });
    }



    public async Task<bool> VerifyOtpAsync()
    {
        if (string.IsNullOrWhiteSpace(Otp))
        {
            ErrorMessage = "Please enter OTP.";
            return false;
        }


        var token = await ExecuteAsync(async () =>
        {
            return await _authenticationService.VerifyOtpAsync(
                new VerifyOtpRequest
                {
                    ClientId = _authenticationSettings.ClientId,

                    Scope = _authenticationSettings.Scope,

                    MobileNumber = MobileNumber,

                    Otp = Otp
                });
        });


        return token != null;
    }
}
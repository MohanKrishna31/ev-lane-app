using nApps.Futs.Mobile.Shared.Constants;
using System;
using System.Collections.Generic;
using System.Text;

namespace nApps.Futs.Mobile.Features.Customer;

public class UpdateCustomerProfileRequest
{
    public string? FullName { get; set; }

    public string? Email { get; set; }

    public DateTime? DateOfBirth { get; set; }

    public Gender Gender { get; set; }

    public string PreferredLanguage { get; set; } = "en";
}

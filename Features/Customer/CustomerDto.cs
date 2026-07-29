using nApps.Futs.Mobile.Shared.Constants;
using System;
using System.Collections.Generic;
using System.Text;

namespace nApps.Futs.Mobile.Features.Customer;

public class CustomerDto
{
    public Guid Id { get; set; }
    public string MobileNumber { get; set; } = "";
    public string? FullName { get; set; }
    public string? Email { get; set; }
    public string? ProfilePhoto { get; set; }
    public decimal WalletBalance { get; set; }
    public string PreferredLanguage { get; set; } = "";
    public DateTime? DateOfBirth { get; set; }
    public Gender Gender { get; set; }
    public CustomerStatus Status { get; set; }
    public string ReferralCode { get; set; } = "";
    public bool IsProfileCompleted { get; set; }
}

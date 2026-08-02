using System.Text.Json.Serialization;

namespace nApps.Futs.Mobile.Features.Wallet;

public sealed class WalletBalanceDto { public double Balance { get; set; } public string? Currency { get; set; } }
public sealed class PagedWalletTransactions { public List<WalletTransactionDto> Items { get; set; } = []; public long TotalCount { get; set; } }
public sealed class PagedWalletTopUps { public List<WalletTopUpDto> Items { get; set; } = []; public long TotalCount { get; set; } }
public sealed class WalletTransactionDto
{
    public Guid Id { get; set; }
    public int Type { get; set; }
    public int Status { get; set; }
    public double Amount { get; set; }
    public double ClosingBalance { get; set; }
    public string? Currency { get; set; }
    public string? Description { get; set; }
    public DateTime OccurredAt { get; set; }
}
public sealed class WalletTopUpDto
{
    public Guid Id { get; set; }
    public double Amount { get; set; }
    public string? Currency { get; set; }
    public int Status { get; set; }
    public DateTime RequestedAt { get; set; }
    public string? FailureReason { get; set; }
}
public sealed class WalletTopUpCheckoutDto
{
    public Guid WalletTopUpId { get; set; }
    public string? KeyId { get; set; }
    public string? ProviderOrderId { get; set; }
    public long AmountSubunits { get; set; }
    public double Amount { get; set; }
    public string? Currency { get; set; }
    public string? CustomerName { get; set; }
    public string? CustomerEmail { get; set; }
    public string? CustomerMobile { get; set; }
    public string? Description { get; set; }
}
public sealed class CreateWalletTopUpRequest { public double Amount { get; set; } public string IdempotencyKey { get; set; } = Guid.NewGuid().ToString("N"); }
public sealed class VerifyWalletTopUpRequest { public string RazorpayOrderId { get; set; } = ""; public string RazorpayPaymentId { get; set; } = ""; public string RazorpaySignature { get; set; } = ""; }
public sealed class WalletTopUpVerificationResultDto { public WalletTopUpDto? TopUp { get; set; } public WalletBalanceDto? Wallet { get; set; } }
public sealed class RazorpayPaymentResult
{
    [JsonPropertyName("razorpay_order_id")] public string OrderId { get; set; } = "";
    [JsonPropertyName("razorpay_payment_id")] public string PaymentId { get; set; } = "";
    [JsonPropertyName("razorpay_signature")] public string Signature { get; set; } = "";
}

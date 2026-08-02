using nApps.Futs.Mobile.Shared.Services.Api;

namespace nApps.Futs.Mobile.Features.Wallet;

public interface IWalletService
{
    Task<WalletBalanceDto?> GetBalanceAsync();
    Task<PagedWalletTransactions> GetTransactionsAsync();
    Task<PagedWalletTopUps> GetTopUpsAsync();
    Task<WalletTopUpCheckoutDto?> CreateTopUpAsync(double amount);
    Task<WalletTopUpVerificationResultDto?> VerifyAsync(Guid id, RazorpayPaymentResult payment);
}

public sealed class WalletService : IWalletService
{
    private readonly IApiService _api;
    public WalletService(IApiService api) => _api = api;
    public Task<WalletBalanceDto?> GetBalanceAsync() => _api.GetAsync<WalletBalanceDto>("/api/app/wallet/balance");
    public async Task<PagedWalletTransactions> GetTransactionsAsync() => await _api.GetAsync<PagedWalletTransactions>("/api/app/wallet/transactions?SkipCount=0&MaxResultCount=50") ?? new();
    public async Task<PagedWalletTopUps> GetTopUpsAsync() => await _api.GetAsync<PagedWalletTopUps>("/api/app/wallet/top-ups?SkipCount=0&MaxResultCount=20") ?? new();
    public Task<WalletTopUpCheckoutDto?> CreateTopUpAsync(double amount) =>
        _api.PostAsync<CreateWalletTopUpRequest, WalletTopUpCheckoutDto>("/api/app/wallet/top-up", new() { Amount = amount });
    public Task<WalletTopUpVerificationResultDto?> VerifyAsync(Guid id, RazorpayPaymentResult payment) =>
        _api.PostAsync<VerifyWalletTopUpRequest, WalletTopUpVerificationResultDto>($"/api/app/wallet/top-up/{id}/verify", new() { RazorpayOrderId = payment.OrderId, RazorpayPaymentId = payment.PaymentId, RazorpaySignature = payment.Signature });
}

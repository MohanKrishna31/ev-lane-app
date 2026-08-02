using nApps.Futs.Mobile.Shared.ViewModels;

namespace nApps.Futs.Mobile.Features.Wallet;

public sealed class WalletViewModel : BaseViewModel
{
    private readonly IWalletService _service;
    public WalletViewModel(IWalletService service) => _service = service;
    public WalletBalanceDto? Balance { get; private set; }
    public IReadOnlyList<WalletTransactionDto> Transactions { get; private set; } = [];
    public IReadOnlyList<WalletTopUpDto> TopUps { get; private set; } = [];
    public double TopUpAmount { get; set; } = 500;

    public async Task LoadAsync() => await ExecuteAsync(async () =>
    {
        Balance = await _service.GetBalanceAsync();
        Transactions = (await _service.GetTransactionsAsync()).Items;
        TopUps = (await _service.GetTopUpsAsync()).Items;
        OnPropertyChanged(nameof(Balance)); OnPropertyChanged(nameof(Transactions)); OnPropertyChanged(nameof(TopUps));
    });

    public async Task<WalletTopUpCheckoutDto?> CreateTopUpAsync() => TopUpAmount < 1
        ? null
        : await ExecuteAsync(() => _service.CreateTopUpAsync(TopUpAmount));

    public async Task<bool> VerifyAsync(WalletTopUpCheckoutDto checkout, RazorpayPaymentResult payment)
    {
        var result = await ExecuteAsync(() => _service.VerifyAsync(checkout.WalletTopUpId, payment));
        if (result is null) return false;
        await LoadAsync();
        return true;
    }
}

namespace CryptoPro.BotsService.Domain.Entities;

public sealed class BotWorker
{
    private readonly ICryptoProClientService _exchangeClientService;
    private readonly string _currencyPair;
    private readonly decimal _targetPrice;
    private readonly decimal _upperPrice;
    private readonly decimal _bottomPrice;
    private readonly decimal _amount;
    private Timer? _timer;

    public BotWorker(
        ICryptoProClientService exchangeClientService,
        string currencyPair,
        decimal sellPrice,
        decimal upperPrice,
        decimal bottomPrice,
        decimal amount)
    {
        _exchangeClientService = exchangeClientService;
        _currencyPair = currencyPair;
        _targetPrice = sellPrice;
        _upperPrice = upperPrice;
        _bottomPrice = bottomPrice;
        _amount = amount;
    }

    public void Start() =>
        _timer = new Timer(CheckPrice, null, TimeSpan.Zero, TimeSpan.FromSeconds(10));

    private async void CheckPrice(object? state = null)
    {
        var currentPrice = await _exchangeClientService.GetCurrencyPriceAsync(_currencyPair);
        if (currentPrice >= _upperPrice || currentPrice <= _bottomPrice)
        {
            await _exchangeClientService.CreateSellOrderAsync(_currencyPair, _amount, _targetPrice);
            Stop();
        }
    }

    public void Stop() => _timer?.Dispose();
}
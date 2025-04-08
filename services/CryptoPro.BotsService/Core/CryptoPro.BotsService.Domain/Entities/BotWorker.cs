namespace CryptoPro.BotsService.Domain.Entities;

public sealed class BotWorker
{
    private readonly ICryptoProClientService _exchangeClientService;
    private readonly CancellationTokenSource _token;
    private readonly string _currencyPair;
    private readonly decimal _upperPrice;
    private readonly decimal _bottomPrice;
    private readonly decimal _amount;

    public BotWorker(
        ICryptoProClientService exchangeClientService,
        string currencyPair,
        decimal upperPrice,
        decimal bottomPrice,
        decimal amount)
    {
        _exchangeClientService = exchangeClientService;
        _currencyPair = currencyPair;
        _upperPrice = upperPrice;
        _bottomPrice = bottomPrice;
        _amount = amount;
        _token = new CancellationTokenSource();
    }

    public async Task StartAsync()
    {
        await RunPeriodicallyAsync(TimeSpan.FromSeconds(5), _token);
    }

    private async Task CheckPriceAsync(object? state = null)
    {
        var currentPrice = await _exchangeClientService.GetCurrencyPriceAsync(_currencyPair, _token.Token);
        if (currentPrice >= _upperPrice || currentPrice <= _bottomPrice)
        {
            await _exchangeClientService.CreateSellOrderAsync(_currencyPair, _amount, _token.Token);
            Stop();
        }
    }

    public async Task RunPeriodicallyAsync(
        TimeSpan interval,
        CancellationTokenSource cancellationToken)
    {
        while (cancellationToken.IsCancellationRequested is false)
        {
            await CheckPriceAsync();
            await Task.Delay(interval, cancellationToken.Token);
        }
    }

    public void Stop() => _token.Cancel();
}
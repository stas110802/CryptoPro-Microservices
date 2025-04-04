using System.Collections.ObjectModel;
using System.IO;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CryptoPro.WpfApp.MVVM.Models;
using CryptoPro.WpfApp.Types;
using Material.Icons;
using Newtonsoft.Json;
using ScottPlot;
using ScottPlot.Plottables;
using ScottPlot.WPF;
using Color = ScottPlot.Color;

namespace CryptoPro.WpfApp.MVVM.ViewModels.UserControls;

public partial class DetailedCoinDescriptionViewModel : ObservableObject
{
    [ObservableProperty] private string _priceChangeType;
    [ObservableProperty] private decimal _currencyPriceChange;
    [ObservableProperty] private decimal _currencyPriceChangePercentage;

    [ObservableProperty] private Coin _coin;
    [ObservableProperty] private RealCurrency _currency;
    [ObservableProperty] private TimeRange _selectedTimeRange;
    [ObservableProperty] private ChartType _SelectedChartType;
    [ObservableProperty] private MarketChart? _chart;
    [ObservableProperty] private CoinFullData _coinFullData;

    [ObservableProperty] private WpfPlot _volumePlotControl;
    [ObservableProperty] private WpfPlot _plotControl;
    [ObservableProperty] private MaterialIconKind _currencyIcon;
    [ObservableProperty] private System.Windows.Media.Brush _currencyIconColor;

    [ObservableProperty] private ObservableCollection<TimeRange> _timeRanges;
    [ObservableProperty] private ObservableCollection<ChartType> _chartTypes;

    public DetailedCoinDescriptionViewModel(Coin coin, RealCurrency currency)
    {
        _coin = coin;
        _currency = currency;

        var text = File.ReadAllText(@"D:\prices.json");
        var text2 = File.ReadAllText(@"D:\btcInfo.json");

        _chart = JsonConvert.DeserializeObject<MarketChart>(text);
        _coinFullData = JsonConvert.DeserializeObject<CoinFullData>(text2);
        _coinFullData.MarketData.Currency = currency.Id.ToLower();

        _priceChangeType = TypeOfDate[^1];
        SwapToNextPriceChange();
        
        InitializeTimeRanges();
        InitializeChartTypes();
        InitializePlotControl();
        InitializeVolumePlotControl();
        InitializeCurrencyIcon();

        PreviousPageCommand = new RelayCommand(GoToPreviousPage);
        SwapToNextPriceChangeCommand = new RelayCommand(SwapToNextPriceChange);
        //var from = (DateTimeOffset.UtcNow - SelectedTimeRange.Range).ToUnixTimeSeconds().ToString();
        //var to = DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString();
    }

    public static readonly string[] TypeOfDate = ["24h", "7d", "14d", "30d", "1y"];
    public ICommand PreviousPageCommand { get; set; }
    public ICommand SwapToNextPriceChangeCommand { get; set; }

    private void GoToPreviousPage()
    {
        MainWindow.GoToPreviousScreen();
    }


    private void SwapToNextPriceChange()
    {
        decimal currencyPrice = 0;
        decimal CurrencyPricePercentage = 0;
        string type = TypeOfDate[0];

        if (PriceChangeType == TypeOfDate[0])
        {
            type = TypeOfDate[1];
            currencyPrice = _coinFullData.MarketData.CurrencyPriceChange7DInCurrency;
            CurrencyPricePercentage = _coinFullData.MarketData.CurrencyPriceChangePercentage7DInCurrency;
        }
        else if (PriceChangeType == TypeOfDate[1])
        {
            type = TypeOfDate[2];
            currencyPrice = _coinFullData.MarketData.CurrencyPriceChange14DInCurrency;
            CurrencyPricePercentage = _coinFullData.MarketData.CurrencyPriceChangePercentage14DInCurrency;
        }
        else if (PriceChangeType == TypeOfDate[2])
        {
            type = TypeOfDate[3];
            currencyPrice = _coinFullData.MarketData.CurrencyPriceChange30DInCurrency;
            CurrencyPricePercentage = _coinFullData.MarketData.CurrencyPriceChangePercentage30DInCurrency;
        }
        else if (PriceChangeType == TypeOfDate[3])
        {
            type = TypeOfDate[4];
            currencyPrice = _coinFullData.MarketData.CurrencyPriceChange1YInCurrency;
            CurrencyPricePercentage = _coinFullData.MarketData.CurrencyPriceChangePercentage1YInCurrency;
        }
        else if (PriceChangeType == TypeOfDate[4])
        {
            type = TypeOfDate[0];
            currencyPrice = _coinFullData.MarketData.CurrencyPriceChange24HInCurrency;
            CurrencyPricePercentage = _coinFullData.MarketData.CurrencyPriceChangePercentage24HInCurrency;
        }

        SetCurrencyPriceChange(type, currencyPrice, CurrencyPricePercentage);
    }

    private void SetCurrencyPriceChange(string type, decimal currencyPriceChange, decimal currencyPriceChangePercentage)
    {
        PriceChangeType = type;
        CurrencyPriceChange = currencyPriceChange;
        CurrencyPriceChangePercentage = currencyPriceChangePercentage;
    }

    private void InitializeChartTypes()
    {
        ChartTypes = new ObservableCollection<ChartType>();
        ChartTypes.Add(ChartType.PriceChart);
        ChartTypes.Add(ChartType.CandlestickChart);
        SelectedChartType = ChartTypes[0];
    }

    private void InitializeTimeRanges()
    {
        SelectedTimeRange = new TimeRange(TimeSpan.FromDays(30), "1M");
        TimeRanges =
        [
            new TimeRange(TimeSpan.FromDays(1), "1D"),
            new TimeRange(TimeSpan.FromDays(7), "7D"),
            new TimeRange(TimeSpan.FromDays(30), "1M"),
            new TimeRange(TimeSpan.FromDays(90), "3M"),
            new TimeRange(TimeSpan.FromDays(365), "1Y")
        ];
    }

    private void InitializeCurrencyIcon()
    {
        if (Currency.Id.ToLower() == "usd")
        {
            CurrencyIcon = MaterialIconKind.Dollar;
            CurrencyIconColor = System.Windows.Media.Brushes.GreenYellow;
        }

        if (Currency.Id.ToLower() == "eur")
        {
            CurrencyIcon = MaterialIconKind.Euro;
            CurrencyIconColor = System.Windows.Media.Brushes.RoyalBlue;
        }
    }

    private void InitializePlotControl()
    {
        PlotControl = new WpfPlot();

        var data = Chart.Prices;

        DateTime[] xs = new DateTime[data.Count()]; //Generate.ConsecutiveDays(data.Count(), start);
        double[] ys = new double[data.Count()];

        for (var i = 0; i < data.Count(); i++)
        {
            ys[i] = (double)data[i][1];
            xs[i] = UnixTimeStampToDateTime((double)data[i][0]);
        }

        var maxSpreadPrice = ys[0];

        for (var i = 0; i < ys.Length - 1; i++)
        {
            double[] numbers = [ys[i], ys[i + 1]];
            DateTime[] dates = [xs[i], xs[i + 1]];

            var isFirstMore = numbers[0] > maxSpreadPrice && numbers[1] < maxSpreadPrice;
            var isSecondMore = numbers[0] < maxSpreadPrice && numbers[1] > maxSpreadPrice;
            if (isFirstMore || isSecondMore)
            {
                var fColor = isFirstMore ? Colors.Green : Colors.Red;
                var sColor = fColor == Colors.Red ? Colors.Green : Colors.Red;
                DivideSpread(numbers, dates, fColor, sColor, maxSpreadPrice);
            }
            else
            {
                var color = numbers[1] >= maxSpreadPrice ? Colors.Green : Colors.Red;
                var sp = PlotControl.Plot.Add.Scatter(dates, numbers, color);
                SetDefaultScatter(sp, maxSpreadPrice);
            }
        }

        AddMaxSpreadLine(maxSpreadPrice, xs);

        PlotControl.Plot.Grid.YAxis.Label.Text = $"Price ({Currency.Id.ToUpper()})";
        PlotControl.Plot.Axes.DateTimeTicksBottom();
        PlotControl.Plot.FigureBackground.Color = Colors.Transparent;
        PlotControl.Plot.DataBackground.Color = Colors.Transparent;
        PlotControl.Plot.Axes.Right.MajorTickStyle.Color = Colors.Transparent;
        PlotControl.Plot.Axes.Right.MinorTickStyle.Color = Colors.Transparent;
        PlotControl.Plot.Axes.Bottom.MajorTickStyle.Color = Colors.Red;
        PlotControl.Plot.Axes.Color(Colors.DimGray); // Text color
        PlotControl.Plot.Axes.FrameWidth(0);
        PlotControl.Plot.Grid.MajorLineColor = Colors.DimGray; // Vertical lines
        PlotControl.Plot.Grid.YAxis = PlotControl.Plot.Axes.Right;
        PlotControl.UserInputProcessor.Disable(); // turn off interaction with chart
        PlotControl.Refresh();
    }

    private void InitializeVolumePlotControl()
    {
        VolumePlotControl = new WpfPlot();

        var data = Chart.TotalVolumes;

        DateTime[] xs = new DateTime[data.Count()]; //Generate.ConsecutiveDays(data.Count(), start);
        double[] ys = new double[data.Count()];

        for (var i = 0; i < data.Count(); i++)
        {
            ys[i] = GetCountOfNumber((double)data[i][1]);
            xs[i] = UnixTimeStampToDateTime((double)data[i][0]);
        }

        var scatter = VolumePlotControl.Plot.Add.Scatter(xs, ys, Colors.LightBlue);
        scatter.LinePattern = LinePattern.Solid;
        scatter.LineWidth = 1f;
        scatter.MarkerSize = 0;
        scatter.FillY = true;
        scatter.FillYValue = 0;
        scatter.FillYAboveColor = Colors.LightBlue.WithAlpha(.6);
        scatter.FillYBelowColor = Colors.LightBlue.WithAlpha(.6);

        VolumePlotControl.Plot.Axes.DateTimeTicksBottom();
        VolumePlotControl.Plot.FigureBackground.Color = Colors.Transparent;
        VolumePlotControl.Plot.DataBackground.Color = Colors.Transparent;
        VolumePlotControl.Plot.Axes.Right.MajorTickStyle.Color = Colors.Transparent;
        VolumePlotControl.Plot.Axes.Right.MinorTickStyle.Color = Colors.Transparent;
        VolumePlotControl.Plot.Axes.Bottom.MajorTickStyle.Color = Colors.Transparent;
        VolumePlotControl.Plot.Axes.Color(Colors.Transparent); // Remove cells
        VolumePlotControl.Plot.Axes.FrameWidth(0);
        VolumePlotControl.Plot.Grid.MajorLineColor = Colors.Transparent; // Remove all text
        VolumePlotControl.Plot.Grid.YAxis = VolumePlotControl.Plot.Axes.Right;
        VolumePlotControl.UserInputProcessor.Disable(); // turn off interaction with chart
        VolumePlotControl.Plot.Grid.YAxis.Label.Text = " ";
        VolumePlotControl.Refresh();
    }

    private void AddMaxSpreadLine(double maxSpreadPrice, DateTime[] allDateTimes)
    {
        double[] lineNumbers = [maxSpreadPrice, maxSpreadPrice];
        DateTime[] lineDates = [allDateTimes[0], allDateTimes[^1]];
        var maxLine = PlotControl.Plot.Add.Scatter(lineDates, lineNumbers, Colors.DimGray);
        maxLine.LinePattern = LinePattern.Dotted;
        SetDefaultScatter(maxLine, maxSpreadPrice);
    }

    private void DivideSpread(double[] numbers, DateTime[] dates, Color firstColor, Color secondColor,
        double spreadPrice)
    {
        double[] numbers2 = [numbers[0], spreadPrice];
        var spErr = PlotControl.Plot.Add.Scatter(dates, numbers2, firstColor);
        SetDefaultScatter(spErr, spreadPrice);

        double[] numbers3 = [spreadPrice, numbers[1]];
        var spErr2 = PlotControl.Plot.Add.Scatter(dates, numbers3, secondColor);
        SetDefaultScatter(spErr2, spreadPrice);
    }

    private void SetDefaultScatter(Scatter scatter, double spreadPrice)
    {
        scatter.LineWidth = 2f;
        scatter.MarkerSize = 0;
        scatter.FillY = true;
        scatter.FillYValue = spreadPrice;
        scatter.FillYAboveColor = Colors.Green.WithAlpha(.1);
        scatter.FillYBelowColor = Colors.Red.WithAlpha(.1);
    }

    private double GetCountOfNumber(double number)
    {
        // a billion
        if (number / 1_000_000_000 > 0)
        {
            return number / 1_000_000_000;
        }

        // a million
        if (number / 1_000_000 > 0)
        {
            return number / 1_000_000;
        }

        // a thousand
        if (number / 1_000 > 0)
        {
            return number / 1_000;
        }

        return number;
    }

    private static DateTime UnixTimeStampToDateTime(double unixTime)
    {
        DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
        dateTime = dateTime.AddMilliseconds(unixTime).ToLocalTime();

        return dateTime;
    }
}
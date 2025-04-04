using System.Windows.Media;
using Newtonsoft.Json;
using ScottPlot;
using ScottPlot.WPF;
using Color = ScottPlot.Color;
using Colors = ScottPlot.Colors;

namespace CryptoPro.WpfApp.MVVM.Models;

public class Coin
{
    public string ExchangeCoinId { get; set; } = string.Empty;
    public string ShortName { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
    public string ImageUri { get; set; } = string.Empty;
    public decimal CurrentPrice { get; set; }
    public long MarketCap { get; set; }
    public float TotalVolume { get; set; }
    public float PriceChangePercentage24h { get; set; }
    public float PriceChangePercentage7d { get; set; }
    public float CirculatingSupply { get; set; }
    public Sparkline? SparklineIn7d { get; set; }

    [JsonIgnore] public int Id { get; set; }
    [JsonIgnore] public WpfPlot PlotControl { get; private set; }

    public void InitPlotControl()
    {
        PlotControl = new WpfPlot();
        var start = DateTime.Now;

        DateTime[] xs = Generate.ConsecutiveDays(SparklineIn7d.Prices.Count, start);
        double[] ys = SparklineIn7d.Prices.ToArray();

        var color = GetScottPlotColorByValue(PriceChangePercentage7d);

        var signal = PlotControl.Plot.Add.SignalXY(xs, ys, color);
        signal.LineWidth = 2f;

        PlotControl.Plot.Axes.DateTimeTicksBottom();
        PlotControl.Plot.FigureBackground.Color = Colors.Transparent;
        PlotControl.Plot.DataBackground.Color = Colors.Transparent;
        PlotControl.Plot.Axes.Right.MajorTickStyle.Color = Colors.Transparent;
        PlotControl.Plot.Axes.Right.MinorTickStyle.Color = Colors.Transparent;
        PlotControl.Plot.Axes.Bottom.MajorTickStyle.Color = Colors.Transparent;
        PlotControl.Plot.Axes.Color(Colors.Transparent); // Remove cells
        PlotControl.Plot.Axes.FrameWidth(0);
        PlotControl.Plot.Grid.MajorLineColor = Colors.Transparent; // Remove all text
        PlotControl.Plot.Grid.YAxis = PlotControl.Plot.Axes.Right;
        PlotControl.UserInputProcessor.Disable(); // turn off interaction with chart
        PlotControl.Refresh();
    }
    
    private Color GetScottPlotColorByValue(float value)
    {
        return value > 0 ? Colors.Green : Colors.Red;
    }
}
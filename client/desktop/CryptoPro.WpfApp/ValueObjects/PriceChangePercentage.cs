using System.Windows.Media;

namespace CryptoPro.WpfApp.ValueObjects;

public sealed class PriceChangePercentage
{
    public double ChangePercentage { get; set; }
    public Brush PercentageColor { get; set; } = Brushes.Red;
}
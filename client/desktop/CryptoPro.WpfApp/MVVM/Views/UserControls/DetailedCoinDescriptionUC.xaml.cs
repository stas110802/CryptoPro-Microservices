using System.Diagnostics;
using System.IO;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using CryptoPro.WpfApp.MVVM.Models;
using CryptoPro.WpfApp.MVVM.ViewModels.UserControls;

namespace CryptoPro.WpfApp.MVVM.Views.UserControls;

public partial class DetailedCoinDescriptionUC : UserControl
{
    public static DetailedCoinDescriptionViewModel ViewModel { get; set; }

    public DetailedCoinDescriptionUC(Coin coin, RealCurrency currency)
    {
        InitializeComponent();
        ViewModel = new DetailedCoinDescriptionViewModel(coin, currency);
        DataContext = ViewModel;
        var path = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\.."));
        FontFamily = new FontFamily(Path.Combine(path, "Fonts", "#Inter"));
    }

    private void FollowOnGithub(object sender, MouseButtonEventArgs e)
    {
        Process.Start(
            new ProcessStartInfo("cmd", $"/c start https://github.com/stas110802")
                { CreateNoWindow = true });
    }

    private void FollowOnTelegram(object sender, MouseButtonEventArgs e)
    {
        Process.Start(
            new ProcessStartInfo(
                    "cmd", $"/c start https://t.me/Akira4kaKilla")
                { CreateNoWindow = true });
    }
}
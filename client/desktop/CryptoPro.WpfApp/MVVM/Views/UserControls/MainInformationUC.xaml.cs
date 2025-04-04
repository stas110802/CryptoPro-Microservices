using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using CommunityToolkit.Mvvm.Input;
using CryptoPro.WpfApp.MVVM.Models;
using CryptoPro.WpfApp.MVVM.ViewModels.UserControls;
using ScottPlot;
using Colors = System.Windows.Media.Colors;

namespace CryptoPro.WpfApp.MVVM.Views.UserControls;

public partial class MainInformationUC : UserControl
{
    public static MainInformationViewModel ViewModel { get; set; }
    public AsyncRelayCommand<RealCurrency> SelectCurrencyCommand { get; set; }

    public MainInformationUC()
    {
        InitializeComponent();
        var path = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\.."));
        FontFamily = new FontFamily(Path.Combine(path, "Fonts", "#Inter"));
        ViewModel = new MainInformationViewModel();
        DataContext = ViewModel;
        SelectCurrencyCommand = new AsyncRelayCommand<RealCurrency>(ChangeSelectedCurrency);
    }

    private void ToggleCurrencyDropdown(object sender, MouseEventArgs e)
    {
        ViewModel.ChangeCurrencySelectVisibility();
    }

    private async Task ChangeSelectedCurrency(RealCurrency? currency)
    {
        if (currency != null)
            await ViewModel.ChangeSelectedCurrency(currency);
    }

    private void CoinsOnMouseUp(object sender, MouseButtonEventArgs e)
    {
        ViewModel.OpenDetailCoinDescriptionPage();
    }
}
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CryptoPro.WpfApp.MVVM.Models;
using CryptoPro.WpfApp.MVVM.Views.UserControls;
using ScottPlot;
using ScottPlot.WPF;

namespace CryptoPro.WpfApp.MVVM.ViewModels.UserControls;

public partial class MainInformationViewModel : ObservableObject
{
    [ObservableProperty] private int _currentPage;
    [ObservableProperty] private string _globalMarketCapText;

    [ObservableProperty] private ObservableCollection<Coin> _coins;
    [ObservableProperty] private ObservableCollection<Coin> _currentPageCoins;
    [ObservableProperty] private ObservableCollection<RealCurrency> _currencies;

    [ObservableProperty] private Visibility _loadingVisibility;
    [ObservableProperty] private Visibility _previousPageButtonVisibility;
    [ObservableProperty] private Visibility _nextPageButtonVisibility;
    [ObservableProperty] private bool _isSelectedCurrencyOpen;

    [ObservableProperty] private RealCurrency? _selectedCurrency;
    [ObservableProperty] private Coin? _selectedCoin;
    
    private readonly Dispatcher _dispatcher;
    
    public MainInformationViewModel(int currentPage = 1)
    {
        _dispatcher = Dispatcher.CurrentDispatcher;
        Thread.CurrentThread.CurrentCulture = Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");
        
        _currencies = new()
        {
            new RealCurrency("US Dollar", "$", "usd"),
            new RealCurrency("Euro", "€", "eur")
        };
        _isSelectedCurrencyOpen = false;
        _selectedCurrency = _currencies.First();
        _globalMarketCapText = "$2.17T";
        _loadingVisibility = Visibility.Collapsed;
        _previousPageButtonVisibility = Visibility.Collapsed;
        _nextPageButtonVisibility = Visibility.Visible;

        _currentPage = currentPage;

        PreviousPageCommand = new RelayCommand(ScrollToPreviousPage);
        NextPageCommand = new RelayCommand(ScrollToNextPage);

        var text = File.ReadAllText(@"D:\usd.json");
        _coins = text.FromJsonWithAutoIncrementId();

        _currentPageCoins = new ObservableCollection<Coin>();

        UpdatePaginatedItems();
    }

    public const short PageSize = 25;

    public ICommand PreviousPageCommand { get; private set; }
    public ICommand NextPageCommand { get; private set; }

    public int TotalPages => (int)Math.Ceiling((double)Coins.Count / PageSize);

    public void ChangeCurrencySelectVisibility()
    {
        IsSelectedCurrencyOpen = !IsSelectedCurrencyOpen;
    }

    public void OpenDetailCoinDescriptionPage()
    {
        if(SelectedCoin != null && SelectedCurrency != null)
        {
            MainWindow.GoToNextScreen(new DetailedCoinDescriptionUC(SelectedCoin, SelectedCurrency));
            SelectedCoin = null;
        }
    }

    public async Task ChangeSelectedCurrency(RealCurrency currency)
    {
        if (SelectedCurrency == currency)
        {
            IsSelectedCurrencyOpen = false;
            return;
        }
        
        var text = File.ReadAllText($@"D:\{currency.Id}.json");
        Coins = text.FromJsonWithAutoIncrementId();
        
        await _dispatcher.InvokeAsync(() =>
        {
            var culture = currency.Id switch
            {
                "eur" => "en-IE",
                "usd" or _ => "en-US"
            };

            Thread.CurrentThread.CurrentCulture = Thread.CurrentThread.CurrentUICulture = new CultureInfo(culture);
        });
        
        SelectedCurrency = currency;
        await Task.Delay(1);
        IsSelectedCurrencyOpen = false;
        UpdatePaginatedItems();
    }

    private void ScrollToNextPage()
    {
        if (CurrentPage < TotalPages)
        {
            CurrentPage += 1;
            MakePageButtonVisible();
            UpdatePaginatedItems();
        }

        if (CurrentPage == TotalPages)
        {
            NextPageButtonVisibility = MakeInvisible();
        }
    }

    private void ScrollToPreviousPage()
    {
        if (CurrentPage > 1)
        {
            CurrentPage -= 1;
            MakePageButtonVisible();
            UpdatePaginatedItems();
        }

        if (CurrentPage == 1)
        {
            PreviousPageButtonVisibility = MakeInvisible();
        }
    }

    private void UpdatePaginatedItems()
    {
        CurrentPageCoins.Clear();

        var itemsToDisplay = Coins.Skip((CurrentPage - 1) * PageSize).Take(PageSize);
        foreach (var item in itemsToDisplay)
        {
            item.InitPlotControl();
            CurrentPageCoins.Add(item);
            item.PlotControl.Refresh();
        }
    }


    private void MakePageButtonVisible()
    {
        NextPageButtonVisibility = Visibility.Visible;
        PreviousPageButtonVisibility = Visibility.Visible;
    }

    private static Visibility MakeVisible()
    {
        return Visibility.Visible;
    }

    private static Visibility MakeInvisible()
    {
        return Visibility.Collapsed;
    }
}
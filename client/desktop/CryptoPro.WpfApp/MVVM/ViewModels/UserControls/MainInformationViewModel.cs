using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CryptoPro.WpfApp.MVVM.Models;
using CryptoPro.WpfApp.ValueObjects;

namespace CryptoPro.WpfApp.MVVM.ViewModels.UserControls;

public partial class MainInformationViewModel : ObservableObject
{
    [ObservableProperty] 
    private int _currentPage;
    
    [ObservableProperty] 
    private ObservableCollection<Coin> _coins;
    [ObservableProperty] 
    private ObservableCollection<Coin> _currentPageCoins;
    
    [ObservableProperty] 
    private Visibility _loadingVisibility;
    [ObservableProperty] 
    private Visibility _previousPageButtonVisibility;
    [ObservableProperty] 
    private Visibility _nextPageButtonVisibility;
    
    public MainInformationViewModel()
    {
        _loadingVisibility = Visibility.Collapsed;
        _previousPageButtonVisibility = Visibility.Collapsed;
        _nextPageButtonVisibility = Visibility.Visible;

        _currentPage = 1;
           
        PreviousPageCommand = new RelayCommand(ScrollToPreviousPage);
        NextPageCommand = new RelayCommand(ScrollToNextPage);

        _coins = new ObservableCollection<Coin>();
        _currentPageCoins = new ObservableCollection<Coin>();

        InitTest();
        UpdatePaginatedItems();
    }

    public const short PageSize = 25;

    public ICommand PreviousPageCommand { get; private set; }
    public ICommand NextPageCommand { get; private set; }

    public int TotalPages => (int)Math.Ceiling((double)Coins.Count / PageSize);

    private void ScrollToNextPage()
    {
        if (CurrentPage < TotalPages)
        {
            CurrentPage += 1;
            MakePageButtonVisible();
            UpdatePaginatedItems();
        }
        if(CurrentPage == TotalPages)
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
        if(CurrentPage == 1)
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
            CurrentPageCoins.Add(item);
        }
    }

    private void InitTest()
    {       
        for (int i = 1; i <= 111; i++)
        {
            Coins.Add(new Coin
            {
                Id = i,
                FullName = $"Bitcoin {i}",
                ImagePath = "F:\\soul\\7bf9266f95288cb1f792e328e37253f0.jpg",
                Name = $"BTC{i}",
                Price = 87_818,
                Volume24H = 29_838_294_014,
                CirculatingSupply = 19_841_762,
                MarketCap = 1_743_644_070_183,
                PriceChangePercentage24H = new PriceChangePercentage()
                {
                    PercentageColor = Brushes.Green,
                    ChangePercentage = 0.35d
                },
                PriceChangePercentage7D = new PriceChangePercentage()
                {
                    PercentageColor = Brushes.Red,
                    ChangePercentage = -4.33d
                }
            });
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
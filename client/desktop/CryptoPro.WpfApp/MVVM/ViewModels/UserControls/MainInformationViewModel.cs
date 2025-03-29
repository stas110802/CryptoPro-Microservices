using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CryptoPro.WpfApp.MVVM.Models;
using CryptoPro.WpfApp.ValueObjects;
using Newtonsoft.Json;

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

        var text = File.ReadAllText(@"D:\response_1743278204571.json");
        _coins = text.FromJsonWithAutoIncrementId();
        
        _currentPageCoins = new ObservableCollection<Coin>();
        
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
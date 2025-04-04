using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using CryptoPro.WpfApp.MVVM.Views.UserControls;

namespace CryptoPro.WpfApp;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private static volatile Frame MainScreenFrame;
    private static volatile UserControl TotalPage;
    private static volatile UserControl? PreviousPage;

    public MainWindow()
    {
        InitializeComponent();
        MainScreenFrame = ScreenFrame;
        TotalPage = new MainInformationUC();
        MainScreenFrame.Navigate(TotalPage);
    }

    private void ProfileMenuItemMouseEnter(object sender, MouseEventArgs e)
    {
        ProfileMenuItem.Header = "\u25b2";
        ProfileMenuItem.Foreground = Brushes.Green;
        ProfileMenuItem.IsSubmenuOpen = true;
    }

    private void ProfileMenuItemMouseLeave(object sender, MouseEventArgs e)
    {
        ProfileMenuItem.Header = "\u25bc";
        ProfileMenuItem.Foreground = Brushes.Red;
        ProfileMenuItem.IsSubmenuOpen = false;
    }

    public static void GoToPreviousScreen()
    {
        if (PreviousPage is not null)
        {
            TotalPage = PreviousPage;
            PreviousPage = null;
            MainScreenFrame.Navigate(TotalPage);
        }
    }

    public static void GoToNextScreen(UserControl nextPage)
    {
        PreviousPage = TotalPage;
        TotalPage = nextPage;
        MainScreenFrame.Navigate(TotalPage);
    }
}


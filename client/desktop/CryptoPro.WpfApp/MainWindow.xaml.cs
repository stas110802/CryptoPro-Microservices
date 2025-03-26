using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using CryptoPro.WpfApp.MVVM.Views.UserControls;

namespace CryptoPro.WpfApp;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public static volatile Frame MainScreenFrame;

    public MainWindow()
    {
        InitializeComponent();
        MainScreenFrame = ScreenFrame;
        ScreenFrame.Navigate(new MainInformationUC());
        var path = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\.."));
        FontFamily = new FontFamily(Path.Combine(path, "Fonts", "#Inter"));
    }
    
}
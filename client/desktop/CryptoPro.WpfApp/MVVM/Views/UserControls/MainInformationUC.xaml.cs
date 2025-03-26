using System.IO;
using System.Windows.Controls;
using System.Windows.Media;
using CryptoPro.WpfApp.MVVM.ViewModels.UserControls;

namespace CryptoPro.WpfApp.MVVM.Views.UserControls;

public partial class MainInformationUC : UserControl
{
    public MainInformationUC()
    {
        InitializeComponent();
        var path = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\.."));
        FontFamily = new FontFamily(Path.Combine(path, "Fonts", "#Inter"));
        DataContext = new MainInformationViewModel();
    }
}
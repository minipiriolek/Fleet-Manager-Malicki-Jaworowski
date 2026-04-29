using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace FleetManager.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        AvaloniaXamlLoader.Load(this);
    }
}

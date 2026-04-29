using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using FleetManager.Models;

namespace FleetManager.Views;

public partial class VehicleItemView : UserControl
{
    public VehicleItemView()
    {
        AvaloniaXamlLoader.Load(this);
    }

    private void RefuelButton_OnClick(object? sender, RoutedEventArgs e)
    {
        if (DataContext is Vehicle vehicle)
        {
            vehicle.Refuel();
        }
    }

    private void DispatchButton_OnClick(object? sender, RoutedEventArgs e)
    {
        if (DataContext is Vehicle vehicle)
        {
            vehicle.Dispatch();
        }
    }
}

using Avalonia.Controls;
using Avalonia.Interactivity;
using FleetManager.Models;

namespace FleetManager.Views;

public partial class VehicleItemView : UserControl
{
    public VehicleItemView()
    {
        InitializeComponent();
    }

    private void RefuelButton_OnClick(object? sender, RoutedEventArgs e)
    {
        if (DataContext is Vehicle vehicle)
            vehicle.Refuel();
    }

    private void DispatchButton_OnClick(object? sender, RoutedEventArgs e)
    {
        if (DataContext is Vehicle vehicle)
            vehicle.Dispatch();
    }

    private void ReturnButton_OnClick(object? sender, RoutedEventArgs e)
    {
        if (DataContext is Vehicle vehicle)
            vehicle.ReturnFromRoute();
    }

    private void ServiceButton_OnClick(object? sender, RoutedEventArgs e)
    {
        if (DataContext is Vehicle vehicle)
            vehicle.SendToService();
    }

    private void FinishServiceButton_OnClick(object? sender, RoutedEventArgs e)
    {
        if (DataContext is Vehicle vehicle)
            vehicle.FinishService();
    }
}

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;
using FleetManager.Models;
using FleetManager.Services;
using ReactiveUI;

namespace FleetManager.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private readonly IVehicleService _vehicleService;
        private readonly List<IDisposable> _vehicleSubscriptions = new();
        private string? _errorMessage;

        public ObservableCollection<Vehicle> Vehicles { get; } = new();

        public string? ErrorMessage
        {
            get => _errorMessage;
            private set
            {
                this.RaiseAndSetIfChanged(ref _errorMessage, value);
                this.RaisePropertyChanged(nameof(HasError));
            }
        }

        public bool HasError => ErrorMessage != null;

        public ReactiveCommand<Unit, Unit> LoadVehiclesCommand { get; }

        public MainWindowViewModel()
            : this(new JsonVehicleService())
        {
        }

        public MainWindowViewModel(IVehicleService vehicleService)
        {
            _vehicleService = vehicleService;
            LoadVehiclesCommand = ReactiveCommand.CreateFromTask(LoadVehiclesAsync);

            _ = LoadVehiclesAsync();
        }

        private async Task LoadVehiclesAsync()
        {
            try
            {
                ErrorMessage = null;

                foreach (var sub in _vehicleSubscriptions)
                    sub.Dispose();
                _vehicleSubscriptions.Clear();

                var data = await _vehicleService.LoadVehiclesAsync();
                Vehicles.Clear();

                foreach (var vehicle in data)
                {
                    Vehicles.Add(vehicle);

                    var subscription = vehicle
                        .WhenAnyValue(v => v.Status, v => v.FuelLevel)
                        .Skip(1)
                        .Subscribe(_ => SaveAsync());

                    _vehicleSubscriptions.Add(subscription);
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Błąd wczytywania danych: {ex.Message}";
            }
        }

        private async Task SaveAsync()
        {
            try
            {
                await _vehicleService.SaveVehiclesAsync(Vehicles);
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Błąd zapisu danych: {ex.Message}";
            }
        }
    }
}

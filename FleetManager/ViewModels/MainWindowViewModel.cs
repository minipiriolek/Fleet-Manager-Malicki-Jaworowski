
using System.Collections.ObjectModel;
using System.Reactive;
using System.Threading.Tasks;
using FleetManager.Models;
using FleetManager.Services;
using ReactiveUI;

namespace FleetManager.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private readonly IVehicleService _vehicleService;
        
        // Kolekcja, którą "widzi" UI
        public ObservableCollection<Vehicle> Vehicles { get; } = new();

        // Komendy dla przycisków
        public ReactiveCommand<Unit, Unit> LoadVehiclesCommand { get; }

        public MainWindowViewModel()
            : this(new JsonVehicleService())
        {
        }

        public MainWindowViewModel(IVehicleService vehicleService)
        {
            _vehicleService = vehicleService;

            // Inicjalizacja komendy ładowania
            LoadVehiclesCommand = ReactiveCommand.CreateFromTask(LoadVehiclesAsync);
            
            // Załaduj dane od razu przy starcie
            Task.Run(() => LoadVehiclesAsync());
        }

        private async Task LoadVehiclesAsync()
        {
            var data = await _vehicleService.LoadVehiclesAsync();
            Vehicles.Clear();
            foreach (var vehicle in data)
            {
                Vehicles.Add(vehicle);
            }
        }
    }
}

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

        public ObservableCollection<Vehicle> Vehicles { get; } = new();

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
            var data = await _vehicleService.LoadVehiclesAsync();
            Vehicles.Clear();

            foreach (var vehicle in data)
            {
                Vehicles.Add(vehicle);
            }
        }

    }
}

using ReactiveUI;


namespace FleetManager.Models
{
    public class Vehicle : ReactiveObject
    {
        private string _name;
        private double _fuelLevel;
        private VehicleStatus _status;

        public string Name
        {
            get => _name;
            set => this.RaiseAndSetIfChanged(ref _name, value);
        }

        public string RegistrationNumber { get; set; }

        public double FuelLevel
        {
            get => _fuelLevel;
            set => this.RaiseAndSetIfChanged(ref _fuelLevel, value);
        }

        public VehicleStatus Status
        {
            get => _status;
            set => this.RaiseAndSetIfChanged(ref _status, value);
        }
    }
}
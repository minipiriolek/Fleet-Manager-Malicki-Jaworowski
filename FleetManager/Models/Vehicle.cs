using Avalonia.Media;
using ReactiveUI;
using System.Text.Json.Serialization;

namespace FleetManager.Models
{
    public class Vehicle : ReactiveObject
    {
        private string _name = string.Empty;
        private double _fuelLevel;
        private VehicleStatus _status;

        public string Name
        {
            get => _name;
            set => this.RaiseAndSetIfChanged(ref _name, value);
        }

        public string RegistrationNumber { get; set; } = string.Empty;

        public double FuelLevel
        {
            get => _fuelLevel;
            set
            {
                var validatedValue = value < 0 ? 0 : value > 100 ? 100 : value;
                this.RaiseAndSetIfChanged(ref _fuelLevel, validatedValue);

                this.RaisePropertyChanged(nameof(FuelColor));
                this.RaisePropertyChanged(nameof(CanGoOnRoute));
                this.RaisePropertyChanged(nameof(CanShowLowFuelWarning));
            }
        }

        public VehicleStatus Status
        {
            get => _status;
            set
            {
                this.RaiseAndSetIfChanged(ref _status, value);

                this.RaisePropertyChanged(nameof(StatusColor));
                this.RaisePropertyChanged(nameof(CanRefuel));
                this.RaisePropertyChanged(nameof(CanGoOnRoute));
                this.RaisePropertyChanged(nameof(CanShowLowFuelWarning));
                this.RaisePropertyChanged(nameof(IsAvailable));
                this.RaisePropertyChanged(nameof(IsInRoute));
                this.RaisePropertyChanged(nameof(IsInService));
            }
        }

        [JsonIgnore]
        public IBrush StatusColor => Status switch
        {
            VehicleStatus.Available => Brushes.Green,
            VehicleStatus.InRoute => Brushes.DodgerBlue,
            VehicleStatus.Service => Brushes.Crimson,
            _ => Brushes.Gray
        };

        [JsonIgnore]
        public IBrush FuelColor => FuelLevel switch
        {
            < 15 => Brushes.Crimson,
            < 40 => Brushes.Orange,
            _ => Brushes.Green
        };

        [JsonIgnore]
        public bool CanRefuel => Status != VehicleStatus.InRoute;

        [JsonIgnore]
        public bool CanGoOnRoute => FuelLevel >= 15 && Status != VehicleStatus.Service;

        [JsonIgnore]
        public bool CanShowLowFuelWarning => !CanGoOnRoute;

        [JsonIgnore]
        public bool IsAvailable => Status == VehicleStatus.Available;

        [JsonIgnore]
        public bool IsInRoute => Status == VehicleStatus.InRoute;

        [JsonIgnore]
        public bool IsInService => Status == VehicleStatus.Service;

        public void Refuel()
        {
            if (CanRefuel)
                FuelLevel = 100;
        }

        public void Dispatch()
        {
            if (CanGoOnRoute)
                Status = VehicleStatus.InRoute;
        }

        public void ReturnFromRoute()
        {
            if (IsInRoute)
                Status = VehicleStatus.Available;
        }

        public void SendToService()
        {
            if (!IsInRoute)
                Status = VehicleStatus.Service;
        }

        public void FinishService()
        {
            if (IsInService)
                Status = VehicleStatus.Available;
        }
    }
}

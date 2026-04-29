using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using FleetManager.Models;

namespace FleetManager.Services
{
    public class JsonVehicleService : IVehicleService
    {
        
        private readonly string _filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "vehicles.json");

        public async Task<IEnumerable<Vehicle>> LoadVehiclesAsync()
        {
            if (!File.Exists(_filePath))
            {
                return new List<Vehicle>(); 
            }

            using FileStream openStream = File.OpenRead(_filePath);
            return await JsonSerializer.DeserializeAsync<List<Vehicle>>(openStream) ?? new List<Vehicle>();
        }

        public async Task SaveVehiclesAsync(IEnumerable<Vehicle> vehicles)
        {
            var options = new JsonSerializerOptions { WriteIndented = true }; 
            using FileStream createStream = File.Create(_filePath);
            await JsonSerializer.SerializeAsync(createStream, vehicles, options);
        }
    }
}

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
        private readonly string _filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets", "vehicles.json");

        public async Task<IEnumerable<Vehicle>> LoadVehiclesAsync()
        {
            if (!File.Exists(_filePath))
                return new List<Vehicle>();

            try
            {
                await using var stream = new FileStream(
                    _filePath, FileMode.Open, FileAccess.Read,
                    FileShare.Read, bufferSize: 4096, FileOptions.Asynchronous);

                return await JsonSerializer.DeserializeAsync<List<Vehicle>>(stream).ConfigureAwait(false)
                       ?? new List<Vehicle>();
            }
            catch (JsonException ex)
            {
                throw new InvalidDataException("Plik vehicles.json jest uszkodzony lub ma nieprawidłowy format.", ex);
            }
            catch (IOException ex)
            {
                throw new IOException("Nie można odczytać pliku vehicles.json.", ex);
            }
        }

        public async Task SaveVehiclesAsync(IEnumerable<Vehicle> vehicles)
        {
            try
            {
                var options = new JsonSerializerOptions { WriteIndented = true };

                await using var stream = new FileStream(
                    _filePath, FileMode.Create, FileAccess.Write,
                    FileShare.None, bufferSize: 4096, FileOptions.Asynchronous);

                await JsonSerializer.SerializeAsync(stream, vehicles, options).ConfigureAwait(false);
            }
            catch (IOException ex)
            {
                throw new IOException("Nie można zapisać pliku vehicles.json.", ex);
            }
        }
    }
}

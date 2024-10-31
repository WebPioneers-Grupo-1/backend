using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using TakeMyCar.Services.Domain.Model.Aggregates;

namespace TakeMyCar.Services.Interfaces.REST
{
    public class CarsApiClient
    {
        private readonly HttpClient _httpClient;

        public CarsApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Car>> GetAllCarsAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<Car>>("api/cars");
        }

        public async Task<Car> GetCarByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<Car>($"api/cars/{id}");
        }

        public async Task<Car> CreateCarAsync(Car car)
        {
            var response = await _httpClient.PostAsJsonAsync("api/cars", car);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<Car>();
        }

        public async Task UpdateCarAsync(Car car)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/cars/{car.Id}", car);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteCarAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/cars/{id}");
            response.EnsureSuccessStatusCode();
        }
    }
}

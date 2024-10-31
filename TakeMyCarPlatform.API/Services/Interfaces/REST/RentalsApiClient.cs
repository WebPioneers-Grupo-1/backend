using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using TakeMyCar.Services.Domain.Model.Aggregates;

namespace TakeMyCar.Services.Interfaces.REST
{
    public class RentalsApiClient
    {
        private readonly HttpClient _httpClient;

        public RentalsApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Rental>> GetAllRentalsAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<Rental>>("api/rentals");
        }

        public async Task<Rental> GetRentalByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<Rental>($"api/rentals/{id}");
        }

        public async Task<Rental> CreateRentalAsync(Rental rental)
        {
            var response = await _httpClient.PostAsJsonAsync("api/rentals", rental);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<Rental>();
        }

        public async Task UpdateRentalAsync(Rental rental)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/rentals/{rental.Id}", rental);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteRentalAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/rentals/{id}");
            response.EnsureSuccessStatusCode();
        }
    }
}

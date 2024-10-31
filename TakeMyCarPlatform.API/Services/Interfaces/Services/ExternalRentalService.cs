using System.Collections.Generic;
using System.Threading.Tasks;
using TakeMyCar.Services.Interfaces.REST;
using TakeMyCar.Services.Domain.Model.Aggregates;

namespace TakeMyCar.Services.Interfaces.Services
{
    public class ExternalRentalService
    {
        private readonly RentalsApiClient _rentalsApiClient;

        public ExternalRentalService(RentalsApiClient rentalsApiClient)
        {
            _rentalsApiClient = rentalsApiClient;
        }

        public async Task<List<Rental>> GetAllRentalsAsync()
        {
            return await _rentalsApiClient.GetAllRentalsAsync();
        }

        public async Task<Rental> GetRentalByIdAsync(int id)
        {
            return await _rentalsApiClient.GetRentalByIdAsync(id);
        }

        public async Task<Rental> AddRentalAsync(Rental rental)
        {
            return await _rentalsApiClient.CreateRentalAsync(rental);
        }

        public async Task UpdateRentalAsync(Rental rental)
        {
            await _rentalsApiClient.UpdateRentalAsync(rental);
        }

        public async Task DeleteRentalAsync(int id)
        {
            await _rentalsApiClient.DeleteRentalAsync(id);
        }
    }
}

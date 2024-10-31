using System.Collections.Generic;
using System.Threading.Tasks;
using TakeMyCar.Services.Domain.Model.Aggregates;
using TakeMyCar.Services.Domain.Repositories;

namespace TakeMyCar.Services.Domain.Services
{
    public class RentalService
    {
        private readonly IRentalRepository _rentalRepository;

        public RentalService(IRentalRepository rentalRepository)
        {
            _rentalRepository = rentalRepository;
        }

        public async Task<Rental> GetRentalByIdAsync(int rentalId)
        {
            return await _rentalRepository.GetByIdAsync(rentalId);
        }

        public async Task<List<Rental>> GetAllRentalsAsync()
        {
            return await _rentalRepository.GetAllAsync();
        }

        public async Task<List<Rental>> GetAvailableRentalsAsync()
        {
            return await _rentalRepository.GetAvailableRentalsAsync();
        }

        public async Task AddRentalAsync(Rental rental)
        {
            await _rentalRepository.AddAsync(rental);
        }

        public async Task UpdateRentalAsync(Rental rental)
        {
            await _rentalRepository.UpdateAsync(rental);
        }

        public async Task DeleteRentalAsync(int rentalId)
        {
            await _rentalRepository.DeleteAsync(rentalId);
        }
    }
}

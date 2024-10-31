using System.Collections.Generic;
using System.Threading.Tasks;
using TakeMyCar.Services.Domain.Model.Aggregates;

namespace TakeMyCar.Services.Domain.Queries
{
    public class GetRentalQuery
    {
        private readonly IRentalRepository _rentalRepository;

        public GetRentalQuery(IRentalRepository rentalRepository)
        {
            _rentalRepository = rentalRepository;
        }

        // Método para obtener un alquiler por su ID
        public async Task<Rental> GetRentalByIdAsync(int rentalId)
        {
            var rental = await _rentalRepository.GetByIdAsync(rentalId);
            if (rental == null)
            {
                throw new RentalNotFoundException($"No se encontró el alquiler con ID: {rentalId}");
            }
            return rental;
        }

        public async Task<List<Rental>> GetAllAvailableRentalsAsync()
        {
            return await _rentalRepository.GetAvailableRentalsAsync();
        }
    }
}

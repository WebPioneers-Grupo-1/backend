using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TakeMyCar.Services.Domain.Model.Aggregates;
using TakeMyCar.Services.Infrastructure.Persistence;

namespace TakeMyCar.Services.Infrastructure.Repositories
{
    public class RentalRepository : IRentalRepository
    {
        private readonly AppDbContext _context;

        public RentalRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Rental> GetByIdAsync(int rentalId)
        {
            return await _context.Rentals.FindAsync(rentalId);
        }

        public async Task<List<Rental>> GetAllAsync()
        {
            return await _context.Rentals.ToListAsync();
        }

        public async Task<List<Rental>> GetAvailableRentalsAsync()
        {
            return await _context.Rentals.Where(r => r.Available == 1).ToListAsync();
        }

        public async Task AddAsync(Rental rental)
        {
            await _context.Rentals.AddAsync(rental);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Rental rental)
        {
            _context.Rentals.Update(rental);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int rentalId)
        {
            var rental = await GetByIdAsync(rentalId);
            if (rental != null)
            {
                _context.Rentals.Remove(rental);
                await _context.SaveChangesAsync();
            }
        }
    }
}

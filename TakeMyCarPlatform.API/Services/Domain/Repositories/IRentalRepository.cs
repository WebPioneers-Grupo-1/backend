using System.Collections.Generic;
using System.Threading.Tasks;
using TakeMyCar.Services.Domain.Model.Aggregates;

namespace TakeMyCar.Services.Domain.Repositories
{
    public interface IRentalRepository
    {
        Task<Rental> GetByIdAsync(int rentalId); 
        Task<List<Rental>> GetAllAsync();         
        Task<List<Rental>> GetAvailableRentalsAsync(); 
        Task AddAsync(Rental rental);           
        Task UpdateAsync(Rental rental);           
        Task DeleteAsync(int rentalId);        
    }
}

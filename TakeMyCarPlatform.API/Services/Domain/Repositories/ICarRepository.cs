using System.Collections.Generic;
using System.Threading.Tasks;
using TakeMyCar.Services.Domain.Model.Aggregates;

namespace TakeMyCar.Services.Domain.Repositories
{
    public interface ICarRepository
    {
        Task<Car> GetByIdAsync(int carId);
        Task<List<Car>> GetAllAsync();    
        Task AddAsync(Car car);             
        Task UpdateAsync(Car car);           
        Task DeleteAsync(int carId);        
    }
}

using System.Collections.Generic;
using System.Threading.Tasks;
using TakeMyCar.Services.Domain.Model.Aggregates;
using TakeMyCar.Services.Domain.Repositories;

namespace TakeMyCar.Services.Domain.Services
{
    public class CarService
    {
        private readonly ICarRepository _carRepository;

        public CarService(ICarRepository carRepository)
        {
            _carRepository = carRepository;
        }

        public async Task<Car> GetCarByIdAsync(int carId)
        {
            return await _carRepository.GetByIdAsync(carId);
        }

        public async Task<List<Car>> GetAllCarsAsync()
        {
            return await _carRepository.GetAllAsync();
        }

        public async Task AddCarAsync(Car car)
        {
            await _carRepository.AddAsync(car);
        }

        public async Task UpdateCarAsync(Car car)
        {
            await _carRepository.UpdateAsync(car);
        }

        public async Task DeleteCarAsync(int carId)
        {
            await _carRepository.DeleteAsync(carId);
        }
    }
}

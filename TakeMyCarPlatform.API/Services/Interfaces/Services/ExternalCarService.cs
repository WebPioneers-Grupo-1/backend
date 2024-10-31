using System.Collections.Generic;
using System.Threading.Tasks;
using TakeMyCar.Services.Interfaces.REST;
using TakeMyCar.Services.Domain.Model.Aggregates;

namespace TakeMyCar.Services.Interfaces.Services
{
    public class ExternalCarService
    {
        private readonly CarsApiClient _carsApiClient;

        public ExternalCarService(CarsApiClient carsApiClient)
        {
            _carsApiClient = carsApiClient;
        }

        public async Task<List<Car>> GetAllCarsAsync()
        {
            return await _carsApiClient.GetAllCarsAsync();
        }

        public async Task<Car> GetCarByIdAsync(int id)
        {
            return await _carsApiClient.GetCarByIdAsync(id);
        }

        public async Task<Car> AddCarAsync(Car car)
        {
            return await _carsApiClient.CreateCarAsync(car);
        }

        public async Task UpdateCarAsync(Car car)
        {
            await _carsApiClient.UpdateCarAsync(car);
        }

        public async Task DeleteCarAsync(int id)
        {
            await _carsApiClient.DeleteCarAsync(id);
        }
    }
}

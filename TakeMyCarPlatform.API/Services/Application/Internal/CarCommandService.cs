using System.Threading.Tasks;
using TakeMyCar.Domain.Entities;
using TakeMyCar.Domain.Repositories;

namespace TakeMyCar.Application.Internal
{
    public class CarCommandService
    {
        private readonly ICarRepository _carRepository;

        public CarCommandService(ICarRepository carRepository)
        {
            _carRepository = carRepository;
        }

        // Método para agregar un nuevo auto
        public async Task<Car> AddCarAsync(string name, decimal price, string url)
        {
            var car = new Car
            {
                Name = name,
                Price = price,
                Url = url,
                Available = 1  // Por defecto disponible
            };

            await _carRepository.AddAsync(car);
            return car;
        }

        public async Task<bool> UpdateCarAsync(int carId, string name, decimal price, string url)
        {
            var car = await _carRepository.GetByIdAsync(carId);
            if (car == null) return false;

            car.Name = name;
            car.Price = price;
            car.Url = url;
            await _carRepository.UpdateAsync(car);
            return true;
        }

        public async Task<bool> DeleteCarAsync(int carId)
        {
            var car = await _carRepository.GetByIdAsync(carId);
            if (car == null) return false;

            await _carRepository.DeleteAsync(car);
            return true;
        }
    }
}

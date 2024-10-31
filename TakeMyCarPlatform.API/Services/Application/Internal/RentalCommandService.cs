using System;
using System.Threading.Tasks;
using TakeMyCar.Domain.Entities;
using TakeMyCar.Domain.Repositories;

namespace TakeMyCar.Application.Internal
{
    public class RentalCommandService
    {
        private readonly IRentalRepository _rentalRepository;
        private readonly ICarRepository _carRepository;

        public RentalCommandService(IRentalRepository rentalRepository, ICarRepository carRepository)
        {
            _rentalRepository = rentalRepository;
            _carRepository = carRepository;
        }

        public async Task<Rental> StartRentalAsync(int carId, int days, bool gps, bool insurance, bool childSeat)
        {
            var car = await _carRepository.GetByIdAsync(carId);
            if (car == null || car.Available == 0) return null;

            car.Available = 0;
            await _carRepository.UpdateAsync(car);

            var rental = new Rental
            {
                CarId = carId,
                StartDate = DateTime.UtcNow,
                EndDate = DateTime.UtcNow.AddDays(days),
                Gps = gps,
                Insurance = insurance,
                ChildSeat = childSeat,
                TotalPrice = CalculateTotalPrice(car.Price, days, gps, insurance, childSeat),
                Status = "Active"
            };

            await _rentalRepository.AddAsync(rental);
            return rental;
        }

        public async Task<bool> CancelRentalAsync(int rentalId)
        {
            var rental = await _rentalRepository.GetByIdAsync(rentalId);
            if (rental == null || rental.Status != "Active") return false;

            var car = await _carRepository.GetByIdAsync(rental.CarId);
            if (car != null)
            {
                car.Available = 1;
                await _carRepository.UpdateAsync(car);
            }

            rental.Status = "Canceled";
            await _rentalRepository.UpdateAsync(rental);
            return true;
        }

        public async Task<bool> EndRentalAsync(int rentalId)
        {
            var rental = await _rentalRepository.GetByIdAsync(rentalId);
            if (rental == null || rental.Status != "Active") return false;

            var car = await _carRepository.GetByIdAsync(rental.CarId);
            if (car != null)
            {
                car.Available = 1;
                await _carRepository.UpdateAsync(car);
            }

            rental.Status = "Completed";
            await _rentalRepository.UpdateAsync(rental);
            return true;
        }

        private decimal CalculateTotalPrice(decimal basePrice, int days, bool gps, bool insurance, bool childSeat)
        {
            decimal total = basePrice * days;
            if (gps) total += 10.00m * days;         // Costo adicional por GPS
            if (insurance) total += 20.00m * days;   // Costo adicional por seguro
            if (childSeat) total += 5.00m * days;    // Costo adicional por asiento infantil
            return total;
        }
    }
}

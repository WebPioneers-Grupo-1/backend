using TakeMyCar.API.Vehicles.Domain.Model.Commands;
using TakeMyCar.API.Vehicles.Domain.Model.Entities;
using TakeMyCar.API.Vehicles.Domain.Repositories;
using TakeMyCar.API.Shared.Domain.Repositories;

namespace TakeMyCar.API.Vehicles.Application.Internal.CommandServices
{
    public class VehicleCommandService : IVehicleCommandService
    {
        private readonly IVehicleRepository _vehicleRepository;
        private readonly IUnitOfWork _unitOfWork;

        public VehicleCommandService(IVehicleRepository vehicleRepository, IUnitOfWork unitOfWork)
        {
            _vehicleRepository = vehicleRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Vehicle?> Handle(CreateVehicleCommand command)
        {
            if (await _vehicleRepository.ExistByLicensePlateAsync(command.LicensePlate))
                throw new Exception("Vehicle already exists");

            var vehicle = new Vehicle(command);
            await _vehicleRepository.AddAsync(vehicle);
            await _unitOfWork.CompleteAsync();
            return vehicle;
        }

        public async Task<Vehicle?> Handle(AddImageAssetToVehicleCommand command)
        {
            var vehicle = await _vehicleRepository.FindByIdAsync(command.VehicleId);
            if (vehicle is null) throw new Exception("Vehicle not found");

            vehicle.AddImage(command.ImageUrl);
            await _unitOfWork.CompleteAsync();
            return vehicle;
        }
    }
}

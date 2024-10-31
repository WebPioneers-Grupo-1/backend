using TakeMyCar.API.Vehicles.Domain.Model.Entities;
using TakeMyCar.API.Vehicles.Domain.Model.Queries;

namespace TakeMyCar.API.Vehicles.Application.Internal.QueryServices
{
    public interface IVehicleQueryService
    {
        Task<Vehicle?> Handle(GetVehicleByIdQuery query);
        Task<IEnumerable<Vehicle>> Handle(GetAllVehiclesQuery query);
    }
}

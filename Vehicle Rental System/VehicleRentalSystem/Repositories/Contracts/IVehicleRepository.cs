namespace VehicleRentalSystem.Repository.Contracts
{
	using VehicleRentalSystem.Models.Contracts;
	public interface IVehicleRepository
	{
		void Add(Vehicle vehicle);
	}
}

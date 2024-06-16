namespace VehicleRentalSystem.Repositories
{
	using VehicleRentalSystem.Models.Contracts;
	using VehicleRentalSystem.Repository.Contracts;
	public class VehicleRepository : IVehicleRepository
	{

		private List<Vehicle> vehicles;

		public VehicleRepository()
		{
			vehicles = new List<Vehicle>();

		}
		public IReadOnlyCollection<Vehicle> Models => vehicles.AsReadOnly();

		public void Add(Vehicle vehicle)
		{
			vehicles.Add(vehicle);
		}
	}
}
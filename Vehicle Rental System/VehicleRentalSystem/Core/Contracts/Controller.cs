namespace VehicleRentalSystem.Core.Contracts
{
	using VehicleRentalSystem.Models;
	using VehicleRentalSystem.Models.Contracts;
	using VehicleRentalSystem.Repositories;
	using VehicleRentalSystem.Repository.Contracts;

	public class Controller : IController
	{
		private readonly IVehicleRepository vehicleRepository;
		private readonly IReservationRepository reservationRepository;

        public Controller()
        {
			vehicleRepository = new VehicleRepository();
			reservationRepository = new ReservationRepository();
        }

		public void AddReservation(Reservation model)
		{
			reservationRepository.Add(model);
		}

		public void AddVehicle(Vehicle model)
		{
			vehicleRepository.Add(model);
		}

		public string PrintReservation(Reservation reservation)
		{
			return reservationRepository.PrintInvoice(reservation);
		}
	}
}

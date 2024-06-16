namespace VehicleRentalSystem.Repository.Contracts
{
	using VehicleRentalSystem.Models;
	public interface IReservationRepository
	{
		public void Add(Reservation model);
		public string PrintInvoice(Reservation reservation);
	}
}

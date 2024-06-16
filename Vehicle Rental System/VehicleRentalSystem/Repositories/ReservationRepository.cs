namespace VehicleRentalSystem.Repositories
{
	using System.Text;
	using VehicleRentalSystem.Models;
	using VehicleRentalSystem.Models.Contracts;
	using VehicleRentalSystem.Repository.Contracts;
	public class ReservationRepository : IReservationRepository
	{
		private readonly List<Reservation> reservationRepository;

		public ReservationRepository()
		{
			reservationRepository = new List<Reservation>();

		}
		public void Add(Reservation model)
		{
			reservationRepository.Add(model);
		}

		public string PrintInvoice(Reservation reservation)
		{
			StringBuilder sb = new StringBuilder();

			decimal insuranceForThePeriod = reservation.CalculateInsurancePrice();
			decimal rentPriceForThePeriod = reservation.CalculateRentPrice();
			reservation.CalculateTotalPrice();

			sb.AppendLine("XXXXXXXXXX");
			sb.AppendLine(reservation.Vehicle.ToString());
			sb.AppendLine();

			sb.AppendLine($"Date: {reservation.ActualReturnDate.ToString("yyyy-MM-dd")}");
			sb.AppendLine($"Customer name: {reservation.FirstName} {reservation.LastName}");
			sb.AppendLine($"Rented Vehicle: {reservation.Vehicle.Brand} {reservation.Vehicle.Model}");
			sb.AppendLine();

			sb.AppendLine($"Reservation start date: {reservation.StartDate.ToString("yyyy-MM-dd")}");
			sb.AppendLine($"Reservation end date: {reservation.EndDate.ToString("yyyy-MM-dd")}");
			sb.AppendLine($"Reserved rental days: {reservation.ReservedDays}");
			sb.AppendLine();

			sb.AppendLine($"Actual return date: {reservation.ActualReturnDate.ToString("yyyy-MM-dd")}");
			sb.AppendLine($"Actual rental days: {reservation.ActualDaysOfUsage}");
			sb.AppendLine();

			sb.AppendLine($"Rent cost per day: ${reservation.Vehicle.DailyPrice:f2}");

			if (reservation.Vehicle.FeeOrDiscount() != null)
			{
				sb.AppendLine(reservation.Vehicle.FeeOrDiscount());
			}

			sb.AppendLine($"Insurance per day: ${insuranceForThePeriod / reservation.ActualDaysOfUsage:f2}");
			sb.AppendLine();

			if (reservation.ActualReturnDate < reservation.EndDate)
			{
				sb.AppendLine($"Early discount for rent: ${reservation.Vehicle.EarlyDiscountForRent:f2}");
				sb.AppendLine($"Early discount for insurance: ${reservation.Vehicle.EarlyDiscountForInsurance:f2}");
				sb.AppendLine();
			}

			sb.AppendLine($"Total rent: ${rentPriceForThePeriod:f2}$");
			sb.AppendLine($"Total insurance: ${insuranceForThePeriod:f2}$");
			sb.AppendLine($"Total: ${reservation.TotalPrice:f2}");
			sb.AppendLine("XXXXXXXXXX");

			return sb.ToString().Trim();
		}
	}
}
namespace VehicleRentalSystem.Models
{
	using VehicleRentalSystem.Models.Contracts;
	public class Reservation : IReservation
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }
		public Vehicle Vehicle { get; set; }
		public int ReservedDays => (EndDate - StartDate).Days;
		public DateTime ActualReturnDate { get; set; }
		public int ActualDaysOfUsage => Math.Abs((StartDate - ActualReturnDate).Days);
		public decimal PriceOfRent { get; set; }
		public decimal PriceOfInsurance { get; set; }
		public decimal TotalPrice { get; set; }

		public Reservation(string firstName, string lastName, DateTime startDate, DateTime endDate, Vehicle vehicle, DateTime actualReturnDate)
		{
			FirstName = firstName;
			LastName = lastName;
			StartDate = startDate;
			EndDate = endDate;
			Vehicle = vehicle;
			ActualReturnDate = actualReturnDate;
			PriceOfRent = 0;
			PriceOfInsurance = 0;
			TotalPrice = 0;
		}
		public void CalculateTotalPrice()
		{
			TotalPrice += PriceOfInsurance + PriceOfRent;
		}

		public decimal CalculateRentPrice()
		{
			decimal totalRentPrice = 0;

			Vehicle.CalculateDiscountRentalCostPerDay(StartDate, EndDate, ActualReturnDate);

			if (ActualReturnDate < EndDate && ActualDaysOfUsage > 7)
			{
				int daysLleft = (EndDate - ActualReturnDate).Days;
				Vehicle.EarlyDiscountForRent = (daysLleft * Vehicle.DailyPrice) / 2;

				totalRentPrice = ActualDaysOfUsage * Vehicle.DailyPrice + Vehicle.EarlyDiscountForRent;
			}
			else if (ActualReturnDate == EndDate && ActualDaysOfUsage > 7)
			{
				totalRentPrice = Vehicle.DailyPrice * ActualDaysOfUsage;
			}
			else if (ActualDaysOfUsage <= 7)
			{
				totalRentPrice = Vehicle.DailyPrice * ActualDaysOfUsage;
			}

			PriceOfRent = totalRentPrice;

			return PriceOfRent;
		}

		public decimal CalculateInsurancePrice()
		{
			decimal insurencePriceForPeriod = 0;

			decimal insurancePricePerDay = Vehicle.CalculateInsuranceCostPerDay();

			if (EndDate == ActualReturnDate)// if brings the vehicle at the return date
			{
				insurencePriceForPeriod = insurancePricePerDay * ReservedDays;
			}
			else // if brings the vehicle before the EndDate
			{
				int daysLeft = EndDate.Day - ActualReturnDate.Day;

				decimal insuranceDiscountForLeftPeriod = insurancePricePerDay * daysLeft;

				Vehicle.EarlyDiscountForInsurance = insuranceDiscountForLeftPeriod;

				insurencePriceForPeriod = insurancePricePerDay * ReservedDays - insurancePricePerDay * daysLeft;
			}

			PriceOfInsurance = insurencePriceForPeriod;

			return insurencePriceForPeriod;
		}

	}
}
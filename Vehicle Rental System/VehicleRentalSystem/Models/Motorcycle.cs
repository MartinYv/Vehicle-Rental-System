namespace VehicleRentalSystem.Models
{
	using System.Text;
	using VehicleRentalSystem.Models.Contracts;
	public class Motorcycle : Vehicle
	{
		private const decimal MOTORCYCLE_DAYLY_PRICE = 15;
		private const decimal MOTORCYCLE_INSURANCE_RATE = (decimal)0.02;

		public int DriverAge { get; set; }
		public decimal InsuranceAdditionPerDay { get; set; }

		public Motorcycle(string brand, string model, decimal vehiclePrice, int driverAge) : base(brand, model, vehiclePrice, MOTORCYCLE_DAYLY_PRICE, MOTORCYCLE_INSURANCE_RATE)
		{
			DriverAge = driverAge;
		}

		public override decimal CalculateInsuranceCostPerDay()
		{
			decimal insuranceCostPerDay = VehiclePrice * MOTORCYCLE_INSURANCE_RATE / 100;

			if (IsUnderAge())
			{
				InsuranceAdditionPerDay = insuranceCostPerDay * 20 / 100;
				insuranceCostPerDay += insuranceCostPerDay * 20 / 100;
			}

			InsuranceCostPerDay = insuranceCostPerDay;

			return insuranceCostPerDay;
		}

		public override decimal CalculateDiscountRentalCostPerDay(DateTime startDate, DateTime endDate, DateTime actualReturnDate)
		{
			int daysOfUsage = Math.Abs((startDate - actualReturnDate).Days);

			if (daysOfUsage > 7)
			{

				this.DailyPrice -= 5;
			}

			return this.DailyPrice;
		}

		public override string ToString()
		{
			return $"A motorcycle valued at {VehiclePrice}$ and the driver is {DriverAge} old";
		}

		public bool IsUnderAge()
		{
			if (DriverAge < 25)
			{
				return true;
			}

			return false;
		}

		public override string? FeeOrDiscount()
		{
			StringBuilder sb = new StringBuilder();

			if (IsUnderAge())
			{
				sb.AppendLine($"Initial insurance per day: ${CalculateInsuranceCostPerDay() - InsuranceAdditionPerDay:f2}");
				sb.AppendLine($"Insurance addition per day: ${InsuranceAdditionPerDay:f2}");

				return sb.ToString().Trim();
			}

			return null;
		}
	}
}
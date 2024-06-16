namespace VehicleRentalSystem.Models
{
	using System.Text;
	using VehicleRentalSystem.Models.Contracts;
	public class CargoVan : Vehicle
	{
		private const decimal CARGO_VAN_DAILY_PRICE = 50;
		private const decimal CARGOVAN_INSURANCE_RATE = (decimal)0.03;

		public int DriverExperience { get; set; }
		public decimal InsuranceDiscountPerDay { get; set; }

		public CargoVan(string brand, string model, decimal vehiclePrice, int driverExperience) : base(brand, model, vehiclePrice, CARGO_VAN_DAILY_PRICE, CARGOVAN_INSURANCE_RATE)
		{
			DriverExperience = driverExperience;
		}

		public override decimal CalculateInsuranceCostPerDay()
		{
			decimal insuranceCostPerDay = VehiclePrice * CARGOVAN_INSURANCE_RATE / 100;

			if (IsExperiencedDriver())
			{
				InsuranceDiscountPerDay = insuranceCostPerDay * 15 / 100;
				insuranceCostPerDay -= insuranceCostPerDay * 15 / 100;
			}

			InsuranceCostPerDay = insuranceCostPerDay;

			return insuranceCostPerDay;
		}

		public override decimal CalculateDiscountRentalCostPerDay(DateTime startDate, DateTime endDate, DateTime actualReturnDate)
		{
			int daysOfUsage = Math.Abs((startDate - actualReturnDate).Days);

			if (daysOfUsage > 7)
			{
				DailyPrice -= 10;
			}

			return DailyPrice;
		}

		public override string ToString()
		{
			return $"A cargo van valued at {VehiclePrice}$ and the driver has {DriverExperience} years of driving experience";
		}

		public bool IsExperiencedDriver()
		{
			if (DriverExperience > 5)
			{
				return true;
			}

			return false;
		}
		public override string? FeeOrDiscount()
		{
			StringBuilder sb = new StringBuilder();

			if (IsExperiencedDriver())
			{
				sb.AppendLine($"Initial insurance per day: ${CalculateInsuranceCostPerDay() + InsuranceDiscountPerDay:f2}");
				sb.AppendLine($"Insurance discount per day: ${InsuranceDiscountPerDay:f2}");

				return sb.ToString().Trim();
			}

			return null;
		}
	}
}

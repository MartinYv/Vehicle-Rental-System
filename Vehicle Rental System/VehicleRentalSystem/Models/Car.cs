namespace VehicleRentalSystem.Models
{
	using System.Text;
	using VehicleRentalSystem.Models.Contracts;
	public class Car : Vehicle
	{
		private const decimal CAR_DAILY_PRICE = 20;
		private const decimal CAR_INSURANCE_RATE = (decimal)0.01;

		private int safetyRating; // Vehicles with star rating of 4 or 5 stars are considered for "High Safety"!!!
		public decimal InsuranceDiscountPerDay { get; set; }

		public Car(string brand, string model, decimal vehiclePrice, int safetyRating) : base(brand, model, vehiclePrice, CAR_DAILY_PRICE, CAR_INSURANCE_RATE)
		{
			SafetyRating = safetyRating;
		}

		public int SafetyRating
		{
			get
			{
				return safetyRating;
			}
			set
			{
				if (value >= 1 && value <= 5)
				{
					safetyRating = value;
				}
				else
				{
					throw new ArgumentException("Safety rating it's out of range.");
				}
			}
		}

		public override decimal CalculateInsuranceCostPerDay()
		{
			decimal insuranceCostPerDay = VehiclePrice * CAR_INSURANCE_RATE / 100;

			if (IsHighSafety())
			{
				InsuranceDiscountPerDay = insuranceCostPerDay * 10 / 100;
				insuranceCostPerDay -= insuranceCostPerDay * 10 / 100;
			}

			InsuranceCostPerDay = insuranceCostPerDay;

			return insuranceCostPerDay;
		}

		public override decimal CalculateDiscountRentalCostPerDay(DateTime startDate, DateTime endDate, DateTime actualReturnDate)
		{
			int daysOfUsage = Math.Abs((startDate - actualReturnDate).Days);

			if (daysOfUsage > 7)
			{
				DailyPrice -= 5;
			}

			return DailyPrice;
		}

		public override string ToString()
		{
			return $"A car valued at {VehiclePrice}$ and has a security rating of {SafetyRating}";
		}

		public override string? FeeOrDiscount()
		{
			StringBuilder sb = new StringBuilder();

			if (IsHighSafety())
			{
				sb.AppendLine($"Initial insurance per day: ${CalculateInsuranceCostPerDay() + InsuranceDiscountPerDay:f2}");
				sb.AppendLine($"Insurance discount per day: ${InsuranceDiscountPerDay:f2}");

				return sb.ToString().TrimEnd();
			}

			return null;
		}

		public bool IsHighSafety()
		{
			if (SafetyRating >= 4 && SafetyRating <= 5)
			{
				return true;
			}

			return false;
		}
	}
}
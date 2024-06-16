namespace VehicleRentalSystem.Models.Contracts
{
	public abstract class Vehicle
	{
		public string Brand { get; set; } = null!;
		public string Model { get; set; } = null!;
		public decimal VehiclePrice { get; set; }
		public decimal DailyPrice { get; set; }
		public decimal DiscountDailyPrice { get; set; }
        public decimal InsuranceRate { get; set; }
        public decimal InsuranceCostPerDay { get; set; }
		public decimal EarlyDiscountForRent { get; set; }
		public decimal EarlyDiscountForInsurance { get; set; }

        public Vehicle(string brand, string model, decimal vehiclePrice, decimal dailyPrice, decimal insuranceRate)
		{
			Brand = brand;
			Model = model;
			VehiclePrice = vehiclePrice;
			DailyPrice = dailyPrice;
			InsuranceRate = insuranceRate;
		}

		public abstract decimal CalculateInsuranceCostPerDay();
		public abstract decimal CalculateDiscountRentalCostPerDay(DateTime startDate, DateTime endDate, DateTime actualReturnDate);
		public abstract string? FeeOrDiscount();
		public override string ToString()
		{
			return base.ToString()!;
		}
	
	}
}

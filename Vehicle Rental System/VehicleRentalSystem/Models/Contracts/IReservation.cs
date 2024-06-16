namespace VehicleRentalSystem.Models.Contracts
{
	public interface IReservation
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }
		public Vehicle Vehicle { get; set; }
		public int ReservedDays { get; }
		public DateTime ActualReturnDate { get; set; }
		public int ActualDaysOfUsage { get; }
		public decimal PriceOfRent { get; set; }
		public decimal PriceOfInsurance { get; set; }
		public decimal TotalPrice { get; set; }
		public decimal CalculateRentPrice();
		public decimal CalculateInsurancePrice();
		public void CalculateTotalPrice();
	}
}

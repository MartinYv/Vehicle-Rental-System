namespace VehicleRentalSystem.IO
{
	using VehicleRentalSystem.IO.Contracts;

	public class Writer : IWriter
	{
		public void Write(string message)
		{
			Console.Write(message);
		}

		public void WriteLine(string message)
		{
			Console.WriteLine(message);
		}
	}
}

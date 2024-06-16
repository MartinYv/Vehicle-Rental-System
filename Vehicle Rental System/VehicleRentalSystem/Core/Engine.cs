namespace VehicleRentalSystem.Core
{
	using System.Globalization;
	using System;
	using VehicleRentalSystem.Core.Contracts;
	using VehicleRentalSystem.Models;
	using VehicleRentalSystem.Models.Contracts;

	public class Engine : IEngine
	{
		private readonly IController controller;

		public Engine()
		{
			controller = new Controller();
		}
		public void Run()
		{
			string folderPath = @"..\..\..\Files\";

			string inputFilePath = Path.Combine(folderPath, "input.txt");
			string outputFilePath = Path.Combine(folderPath, "output.txt");
			string logFilePath = Path.Combine(folderPath, "error.log");

			if (!Directory.Exists(folderPath))
			{
				Directory.CreateDirectory(folderPath);
			}

			if (!File.Exists(inputFilePath))
			{
				throw new FileNotFoundException("The input file does not exist.", inputFilePath);
			}

			if (!File.Exists(outputFilePath))
			{
				using (File.Create(outputFilePath)) { }
			}

			try
			{
				using (StreamReader reader = new StreamReader(inputFilePath))
				using (StreamWriter writer = new StreamWriter(outputFilePath, false))
				{
					string line;
					while ((line = reader.ReadLine()) != "End")
					{
						if (line == null)
						{
							writer.WriteLine("All of the invoices are printed or input it's not provided.");
							break;
						}

						string[] input = line.Split(", ");

						if (input.Length < 10)
						{
							throw new ArgumentException("Missing parameters.");
						}
						if (input.Length > 10)
						{
							throw new ArgumentException("Invalid input.");
						}

						List<string> dates = new List<string>() { input[2], input[3], input[9] };
						bool isDateValid = true;

						for (int i = 0; i < dates.Count; i++)
						{
							string dateString = dates[i];
							DateTime result;
							string format = "yyyy-MM-dd";

							try
							{
								result = DateTime.ParseExact(dateString, format, CultureInfo.InvariantCulture);
							}
							catch (FormatException)
							{
								isDateValid = false;

								writer.WriteLine(($"{dateString} is not in the correct format."));
								break;
							}
						}

						if (!isDateValid)
						{
							continue;
						}

						string userFirstName = input[0];
						string userLastName = input[1];
						DateTime startDate = DateTime.Parse(input[2]);
						DateTime endDate = DateTime.Parse(input[3]);
						string vehicleType = input[5];
						string vehicleBrand = input[6];
						string vehicleModel = input[7];
						decimal vehiclePrice = decimal.Parse(input[8]);
						DateTime actualReturnDate = DateTime.Parse(input[9]);

						Vehicle? vehicle = null;

						int vehicleIntInput;

						if (!int.TryParse(input[4],out vehicleIntInput))
						{
							writer.WriteLine("Parameter 4 should be an integer.");
							continue;
						}


						if (vehicleType == "Car")
						{
							int safetyRating = int.Parse(input[4]);

							if (safetyRating < 1 || safetyRating > 5)
							{
								writer.WriteLine("Safety rating out of range.");
								continue;
							}

							vehicle = new Car(vehicleBrand, vehicleModel, vehiclePrice, safetyRating);
						}
						else if (vehicleType == "Motorcycle")
						{
							int driverAge = int.Parse(input[4]);

							vehicle = new Motorcycle(vehicleBrand, vehicleModel, vehiclePrice, driverAge);
						}
						else if (vehicleType == "Cargovan")
						{
							int driverExperience = int.Parse(input[4]);

							vehicle = new CargoVan(vehicleBrand, vehicleModel, vehiclePrice, driverExperience);
						}
						else
						{
							writer.WriteLine("Invalid vehicle type.");
							continue;
						}

						//if (StartDate< DateTime.Now )
						//{
						//	throw new ArgumentException("Start date it's not valid.");
						//}
						if (actualReturnDate > endDate)
						{
							writer.WriteLine("Cannot return the vehicle after the reservation end date.");
							continue;
						}
						else if (actualReturnDate < startDate)
						{
							writer.WriteLine("Cannot return the vehicle before the reservation start date.");
							continue;
						}

						Reservation reservation = new Reservation(userFirstName, userLastName, startDate, endDate, vehicle, actualReturnDate);

						controller.AddVehicle(vehicle);
						controller.AddReservation(reservation);

						writer.WriteLine(controller.PrintReservation(reservation));
						writer.WriteLine();
					}
				}
			}
			catch (Exception ex)
			{
				using (StreamWriter logWriter = new StreamWriter(logFilePath, true))
				{
					logWriter.WriteLine($"[{DateTime.Now}] An error occurred: {ex.Message}");
					logWriter.WriteLine(ex.ToString());
				}
			}
		}
	}
}

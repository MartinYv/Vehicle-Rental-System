using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleRentalSystem.Models;
using VehicleRentalSystem.Models.Contracts;


namespace VehicleRentalSystem.Core.Contracts
{
	public interface IController
	{
		public void AddVehicle(Vehicle model);
		public void AddReservation(Reservation model);
		public string PrintReservation(Reservation reservation);
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACar.DTOs.Vehicle
{
    public class UpdateVehicleDTO
    {
        public int Id { get; set; }

        public string LicensePlate { get; set; }

        public string Color { get; set; }

        public double Kms { get; set; }

        public double RentalPrice { get; set; }

        public int Hp { get; set; }

    }
}

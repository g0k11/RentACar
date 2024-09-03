using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACar.Domain
{
    internal class Reservations
    {
        public int Id { get; set; }        
        public DateTime ReservationDate { get; set; }
        public DateTime ReceivalDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Status { get; set; }

        //Foreign Keys
        public int VehicleId { get; set; }
        public int RentallerId { get; set; }

        //Relations
        public ICollection<Rentaller> Rentallers { get; set; }
        public ICollection<Vehicle> Vehicles { get; set; }
    }
}

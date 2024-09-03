using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACar.Domain
{
    internal class Garage
    {
        public int Id { get; set; }
        public string GarageName { get; set; }
        public string Location { get; set; }
        public DateTime EstablishDate { get; set; }
        public decimal BalanceSheet { get; set; }




        // Relations

        public ICollection<Vehicle> Vehicles { get; set; }

        public ICollection<Rentaller> Rentallers { get; set; }

        public ICollection<Employers> Employers { get; set; }




    }
}

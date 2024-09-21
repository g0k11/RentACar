using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACar.Domain
{
    public class Payment
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public double Amount { get; set; }
        [Required]
        public string TransactionId { get; set; } = string.Empty;
        [Required]
        public DateTime PaymentDate { get; set; }

        [Required]
        public int UserId { get; set; }
        [Required]
        public User User { get; set; } = new User();
        [Required]
        public int VehicleId { get; set; }
        [Required]
        public Vehicle Vehicle { get; set; } = new Vehicle();
    }

}

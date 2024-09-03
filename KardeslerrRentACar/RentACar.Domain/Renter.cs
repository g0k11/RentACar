using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACar.Domain
{
    public class Renter
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [RegularExpression("^(?=.*[0-9]).{11}$")]
        [DisallowNull]
        public string? GovIdNumber { get; set; }

        [Required]
        [RegularExpression("^[A-Z][0-9]$")]
        [DisallowNull]
        public string? LicenseType { get; set; }

        [Required]        
        public DateTime DateOfBirth { get; set; }

        [Required]
        [StringLength(111, MinimumLength = 2)]
        [DisallowNull]
        public string? Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [RegularExpression("^(?=.*[0-9])(?=.*[a-z])(?=.*[A-Z]).{8,24}$")]
        public string Password { get; set; }

        [Required]
        [Phone]
        public string PhoneNumber { get; set; }

        public string? Gender { get; set; }

        [Required]
        [StringLength (500, MinimumLength = 10)]
        public string? Address { get; set; }
        
        public int RentCount
        {
            get
            {
                if (Reservations.Any())
                {
                    return Reservations.Where(x => x.Status == "Valid").Count();
                }

                return 0;
            }
        }

        // Relations
        [AllowNull]
        public ICollection<Reservations> Reservations { get; set; }





    }
}

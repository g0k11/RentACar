using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACar.Domain
{
    public class Employers
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(55, MinimumLength = 2)]
        public string Name { get; set; } = null!;
        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;
        [Required]
        [RegularExpression("^(?=.*[0-9])(?=.*[a-z])(?=.*[A-Z]).{8,24}$")]
        public string PasswordHashed { get; set; } = null!;
        [Required]
        [Phone]
        public string PhoneNumber { get; set; } = null!;
        [Required]
        public string Role { get; set; } = null!;

        // Relations
        [Required]
        public int GarageId { get; set; }
        [DisallowNull]
        public Garage? Garage { get; set; }
    }
}

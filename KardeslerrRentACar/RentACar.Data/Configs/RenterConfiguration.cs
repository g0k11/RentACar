using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RentACar.Core.Interfaces;
using RentACar.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace RentACar.Data.Configs
{
    public class RenterConfiguration : IEntityTypeConfiguration<Renter>
    {
        public void Configure(EntityTypeBuilder<Renter> builder)
        {
            builder.HasOne(r => r.User)   
               .WithOne()                    
               .HasForeignKey<Renter>(r => r.UserId) 
               .OnDelete(DeleteBehavior.Cascade);
           
        }
      
    }
    public class ReservationConfiguration : IEntityTypeConfiguration<Reservations>
    {
        public void Configure(EntityTypeBuilder<Reservation> builder)
        {
            // Configuring the one-to-many relationship between Renter and Reservations
            builder.HasOne(res => res.Renter)        // Reservation has one Renter
                   .WithMany(r => r.Reservations)    // Renter has many Reservations
                   .HasForeignKey(res => res.RenterId) // Foreign key in Reservation is RenterId
                   .OnDelete(DeleteBehavior.Cascade);  // Cascade delete when a Renter is deleted
        }
    }
}

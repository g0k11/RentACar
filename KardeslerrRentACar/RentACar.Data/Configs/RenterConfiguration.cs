using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RentACar.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
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
}

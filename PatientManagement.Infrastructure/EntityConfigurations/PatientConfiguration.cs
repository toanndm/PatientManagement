using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PatientManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatientManagement.Infrastructure.EntityConfigurations
{
    public class PatientConfiguration : IEntityTypeConfiguration<Patient>
    {
        public void Configure(EntityTypeBuilder<Patient> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .IsRequired();

            builder.Property(x => x.FirstName)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(x => x.LastName)
                .IsRequired()
                .HasMaxLength(50);
            
            builder.Property(x => x.Email)
                .HasMaxLength(50);

            builder.Property(x => x.Phone)
                .IsRequired()
                .HasMaxLength(13);

            builder.Property(x => x.PrimaryAddress)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(x => x.PrimaryProvince)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.PrimaryDistrict)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.PrimaryWard)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.SecondaryAddress)
                .HasMaxLength(150);

            builder.Property(x => x.SecondaryProvince)
                .HasMaxLength(100);

            builder.Property(x => x.SecondaryDistrict)
                .HasMaxLength(100);

            builder.Property(x => x.SecondaryWard)
                .HasMaxLength(100);

            builder.Property(x => x.Description)
                .HasMaxLength(250);

            builder.Property(x => x.Reason)
                .HasMaxLength(250);

            builder.HasIndex(x => x.FirstName);

            builder.HasIndex(x => x.LastName);

            builder.HasIndex(x => x.Phone);

        }
    }
}

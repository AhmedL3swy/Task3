using DataGrid.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace DataGrid.Persistence.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            // Define constraints and validation rules
            builder.HasKey(u => u.Id);

          
            builder.Property(u => u.FirstName)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(u => u.LastName)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(u => u.Mobile)
                .IsRequired()
                .HasMaxLength(10)
                .HasAnnotation("RegularExpression", @"^05\d{8}$");

            builder.Property(u => u.MunicipalNo)
                .IsRequired()
                .HasMaxLength(10)
                .HasAnnotation("RegularExpression", @"^\d{6,10}$");

            builder.Property(u => u.NationalId)
                .IsRequired()
                .HasMaxLength(10)
                .HasAnnotation("RegularExpression", @"^\d{10}$");

            builder.Property(u => u.Address)
                .IsRequired()
                .HasMaxLength(100);

            // Define unique indexes
            builder.HasIndex(u => u.Email).IsUnique();
            builder.HasIndex(u => u.Mobile).IsUnique();
            builder.HasIndex(u => u.MunicipalNo).IsUnique();
            builder.HasIndex(u => u.NationalId).IsUnique();
        }
    }
}

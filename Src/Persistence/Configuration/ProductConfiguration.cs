using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configuration
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(e => e.ProductId)
                .HasName("PK_Product");

            builder.Property(e => e.UserId)
                .HasColumnName("UserID")
                .IsRequired();

            builder.Property(e => e.Name)
                .IsRequired();

            builder.Property(e => e.Desciption)
                .HasColumnType("nvarchar (100)")
                .HasMaxLength(100);

            builder.Property(e => e.Count);

            builder.Property(e => e.Slug);

            builder.Property(e => e.Price)
                .IsRequired();

            builder.HasIndex(p => p.Name)
                .IsUnique()
                .HasDatabaseName("IDX_Name");

            //builder.HasOne(d => d.User)
            //    .WithMany(p => p.Products)
            //    .HasForeignKey(d => d.UserId)
            //    .OnDelete(DeleteBehavior.ClientSetNull)
            //    .HasConstraintName("FK_Product_User");
        }
    }
}
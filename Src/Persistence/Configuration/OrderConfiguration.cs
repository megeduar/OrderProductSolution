using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configuration
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(e => e.OrderId)
                .HasName("PK_Order");

            builder.Property(e => e.UserId)
                .HasColumnName("UserID")
                .IsRequired();

            builder.Property(e => e.ProductId)
                .HasColumnName("ProductID")
                .IsRequired();

            builder.Property(e => e.Count);

            builder.Property(e => e.Date);

            builder.Property(e => e.State);   
        }
    }
}
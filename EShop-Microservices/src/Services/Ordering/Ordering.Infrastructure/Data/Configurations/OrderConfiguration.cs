

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ordering.Domain.Enum;
using Ordering.Domain.Models;
using Ordering.Domain.ValueObjects;

namespace Ordering.Infrastructure.Data.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(o => o.Id); // PK

            builder.Property(o => o.Id).HasConversion( // Value Object Conversion
                orderId => orderId.Value,
                dbId => OrderId.Of(dbId));

            builder.HasOne<Customer>() // Relationship with Customer
                   .WithMany()
                   .HasForeignKey(o => o.CustomerId)
                   .IsRequired();

            builder.HasMany(o => o.OrderItems) // Relationship with OrderItems
                   .WithOne()
                   .HasForeignKey(oi => oi.OrderId);

            builder.ComplexProperty(o => o.OrderName, nameBuilder =>
            {
                nameBuilder.Property(n => n.Value)
                           .HasColumnName(nameof(Order.OrderName))
                           .IsRequired()
                           .HasMaxLength(100);
            }); // Complex Type

            builder.ComplexProperty(o => o.ShippingAddress, addressBuilder =>
            {
                addressBuilder.Property(a => a.FirstName).IsRequired().HasMaxLength(50);
                addressBuilder.Property(a => a.LastName).IsRequired().HasMaxLength(50);
                addressBuilder.Property(a => a.EmailAddress).HasMaxLength(50);
                addressBuilder.Property(a => a.AddressLine).HasMaxLength(180).IsRequired();
                addressBuilder.Property(a => a.Country).HasMaxLength(50);
                addressBuilder.Property(a => a.State).HasMaxLength(50);
                addressBuilder.Property(a => a.ZipCode).HasMaxLength(5).IsRequired();
                // Complex Type
            });

            builder.ComplexProperty(o => o.BillingAddress, addressBuilder =>
            {
                addressBuilder.Property(a => a.FirstName).IsRequired().HasMaxLength(50);
                addressBuilder.Property(a => a.LastName).IsRequired().HasMaxLength(50);
                addressBuilder.Property(a => a.EmailAddress).HasMaxLength(50);
                addressBuilder.Property(a => a.AddressLine).HasMaxLength(180).IsRequired();
                addressBuilder.Property(a => a.Country).HasMaxLength(50);
                addressBuilder.Property(a => a.State).HasMaxLength(50);
                addressBuilder.Property(a => a.ZipCode).HasMaxLength(5).IsRequired();
                // Complex Type
            });

            builder.ComplexProperty(o => o.Payment, paymentBuilder =>
            {
                paymentBuilder.Property(p => p.CardName).HasMaxLength(50);
                paymentBuilder.Property(p => p.CardNumber).IsRequired().HasMaxLength(24);
                paymentBuilder.Property(p => p.Expiration).HasMaxLength(10);
                paymentBuilder.Property(p => p.CVV).HasMaxLength(3);
                paymentBuilder.Property(p => p.PaymentMethod);
                // Complex Type
            });

            builder.Property(o => o.Status).HasDefaultValue(OrderStatus.Draft)
                .HasConversion(
                    s => s.ToString(), // Enum as string
                    dbStatus => (OrderStatus)Enum.Parse(typeof(OrderStatus), dbStatus));

            builder.Property(o => o.TotalPrice);
        }
    }
}

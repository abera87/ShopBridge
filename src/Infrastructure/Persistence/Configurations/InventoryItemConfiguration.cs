using ShopBridge.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ShopBridge.Infrastructure.Persistence.Configurations
{
    public class InventoryItemConfiguration : IEntityTypeConfiguration<InventoryItem>
    {
        public void Configure(EntityTypeBuilder<InventoryItem> builder)
        {
            builder.Property(p => p.Name)
                    .HasMaxLength(200)
                    .IsRequired();

            builder.Property(p=>p.Price)
                    .IsRequired();

            builder.Property(p=>p.Description)
                    .HasMaxLength(4000);

            builder.Property(p=>p.CreatedBy)
                    .HasMaxLength(4000);

            builder.Property(p=>p.LastModifiedBy)
                    .HasMaxLength(4000);
                    
        }
    }
}
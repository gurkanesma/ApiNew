using Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Api.Persistence.Configurations
{
    public class ProductCategoryConfiguration : IEntityTypeConfiguration<ProductCategory>
    {
        public void Configure(EntityTypeBuilder<ProductCategory> builder)
        {
            // Composite Key
            builder.HasKey(x => new { x.ProductId, x.CategoryId });

            // Product ilişkisi
            builder.HasOne(pc => pc.Product)
                   .WithMany(p => p.ProductCategories)
                   .HasForeignKey(pc => pc.ProductId)
                   .OnDelete(DeleteBehavior.Cascade); // Kaskad silmeyi devre dışı bıraktık

            // Category ilişkisi
            builder.HasOne(pc => pc.Category)
                   .WithMany(c => c.ProductCategories)
                   .HasForeignKey(pc => pc.CategoryId)
                   .OnDelete(DeleteBehavior.Restrict); // Kaskad silmeyi devre dışı bıraktık.
        }
    }
}

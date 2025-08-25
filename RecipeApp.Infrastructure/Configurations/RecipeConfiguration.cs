using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RecipeApp.Domain.Entities;

namespace RecipeApp.Infrastructure.Configurations
{
    public class RecipeConfiguration : IEntityTypeConfiguration<Recipe>
    {
        public void Configure(EntityTypeBuilder<Recipe> builder)
        {
            builder.HasKey(r => r.Id);

            builder.Property(r => r.Title)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(r => r.Description)
                .IsRequired()
                .HasMaxLength(5000);

            builder.Property(r => r.PhotoUrl)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(r => r.IsPublic)
                .IsRequired()
                .HasDefaultValue(true);

            builder.Property(r => r.CreatedAt)
                .IsRequired();

            builder.Property(r => r.AuthorId)
                .IsRequired();

            // Relations
            builder.HasOne(r => r.Author)
                .WithMany(u => u.Recipes)
                .HasForeignKey(r => r.AuthorId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(r => r.Category)
                .WithMany(c => c.Recipes)
                .HasForeignKey(r => r.CategoryId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasMany(r => r.Reactions)
                .WithOne(react => react.Recipe)
                .HasForeignKey(react => react.RecipeId)
                .OnDelete(DeleteBehavior.Cascade);

            // Index pour performance
            builder.HasIndex(r => r.CreatedAt);
            builder.HasIndex(r => r.IsPublic);
        }
    }
}
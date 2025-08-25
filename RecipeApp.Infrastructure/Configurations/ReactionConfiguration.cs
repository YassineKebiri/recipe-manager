using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RecipeApp.Domain.Entities;

namespace RecipeApp.Infrastructure.Configurations
{
    public class ReactionConfiguration : IEntityTypeConfiguration<Reaction>
    {
        public void Configure(EntityTypeBuilder<Reaction> builder)
        {
            builder.HasKey(r => r.Id);

            builder.Property(r => r.RecipeId)
                .IsRequired();

            builder.Property(r => r.UserId)
                .IsRequired();

            builder.Property(r => r.Type)
                .IsRequired()
                .HasConversion<int>(); // Stocker l'enum comme int

            builder.Property(r => r.CreatedAt)
                .IsRequired();

            // Contrainte unique : un utilisateur ne peut avoir qu'une seule réaction par recette
            builder.HasIndex(r => new { r.UserId, r.RecipeId }).IsUnique();

            // Relations
            builder.HasOne(r => r.User)
                .WithMany(u => u.Reactions)
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(r => r.Recipe)
                .WithMany(recipe => recipe.Reactions)
                .HasForeignKey(r => r.RecipeId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
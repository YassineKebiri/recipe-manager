using Microsoft.EntityFrameworkCore;
using RecipeApp.Domain.Entities;

namespace RecipeApp.Application.Common
{
    public interface IApplicationDbContext
    {
        DbSet<User> Users { get; }
        DbSet<Recipe> Recipes { get; }
        DbSet<Category> Categories { get; }
        DbSet<Reaction> Reactions { get; }
        
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
using Microsoft.EntityFrameworkCore;
using RecipeApp.Application.Common;
using RecipeApp.Domain.Entities;
using RecipeApp.Domain.Common;
using RecipeApp.Infrastructure.Configurations;

namespace RecipeApp.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users => Set<User>();
        public DbSet<Recipe> Recipes => Set<Recipe>();
        public DbSet<Category> Categories => Set<Category>();
        public DbSet<Reaction> Reactions => Set<Reaction>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new RecipeConfiguration());
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
            modelBuilder.ApplyConfiguration(new ReactionConfiguration());

            base.OnModelCreating(modelBuilder);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            // Publier les Domain Events avant de sauvegarder
            await PublishDomainEvents();
            
            return await base.SaveChangesAsync(cancellationToken);
        }

        private async Task PublishDomainEvents()
        {
            var entities = ChangeTracker.Entries<BaseEntity>()
                .Where(e => e.Entity.DomainEvents.Any())
                .Select(e => e.Entity);

            var domainEvents = entities.SelectMany(e => e.DomainEvents).ToList();

            foreach (var entity in entities)
            {
                entity.ClearDomainEvents();
            }

            // TODO: Publier via MediatR (on le fera plus tard)
            foreach (var domainEvent in domainEvents)
            {
                // await _mediator.Publish(domainEvent, cancellationToken);
            }
        }
    }
}  